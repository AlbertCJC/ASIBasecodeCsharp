# CRUD Implementation Plan — CampusPulse (Alliance Group 5)

Prepared from:
- **Scaffold analysis** — ASI.Basecode solution (WebApp → Services → Data → Resources)
- **Figma file** — *Alliance – Group 5* (`Fif2gCnMIWuqEkEYhgAuUv`, "Initial Deisgn" canvas), rendered reference screenshots saved in `docs/figma/`
- **Database** — `AllianceDB`, reverse-engineered into `AllianceDbContext` + 20 entity classes in `ASI.Basecode.Data/Models/`

> Role naming from the DB/Figma: **A = Administrator**, **B = Borrower (Student/Faculty)**, **C = Custodian (Property Office staff)**, **US = Universal (public/auth)**. All US screens (Landing, Registration, Login, Forgot/Reset Password) are **out of scope** — web auth was intentionally stripped per AGENTS.md.

---

## 1. Where the scaffold stands today

| Layer | State |
|---|---|
| `ASI.Basecode.Data` | `AllianceDbContext` (20 DbSets, full FK config, rowversion concurrency) + entity models. `AsiBasecodeDBContext` still **empty**. Generic `BaseRepository` stub (casts to the *empty* `AsiBasecodeDBContext`). `UnitOfWork` wraps `AsiBasecodeDBContext`. |
| `ASI.Basecode.Services` | Only `ServiceBase` (logger base). No services, view models, or validators. |
| `ASI.Basecode.WebApp` | Only `HomeController` (thin, inherits `ControllerBase<T>`). `_Layout` uses Bootstrap + toastr. Default Home/Index. |
| `ASI.Basecode.Resources` | Resx resolvers exist (`Screen`, `Button`, `Table`, `TitleText`, `Errors`, `Common`) — Screen.resx has login strings; rest are placeholders. |
| DI wiring | `Startup.DI.cs.ConfigureOtherServices` registers only `IUnitOfWork` + `IHttpContextAccessor` + http clients. AutoMapper profile in `Startup.AutoMapper.cs` is **empty**. |

**The critical finding:** everything above the Data layer is untouched, and the registered EF context (`AsiBasecodeDBContext`, DB `AsiBasecodeDB`) does **not** contain the entities — they live in the unregistered `AllianceDbContext` (DB `AllianceDB`). Step 0 fixes this before any CRUD is written.

---

## 2. Step 0 — Context wiring (prerequisite, blocks everything)

Goal: make `UnitOfWork`/`BaseRepository` bind to `AllianceDbContext`.

1. **`appsettings.json`** — add `AllianceConnection` connection string → `AllianceDB` (server, trusted or SQL auth — mirror `DefaultConnection` format).
2. **`Startup.DI.cs`** — replace `AddDbContext<AsiBasecodeDBContext>(o => o.UseSqlServer(...DefaultConnection))` in `Startup.cs` with `AllianceDbContext` bound to `AllianceConnection`; keep the partial-class conventions. (Alternative: dedupe `AllianceDbContext`, copy its `OnModelCreating` body into `AsiBasecodeDBContext.OnModelCreatingPartial`, and keep the registered name. Choose ONE; recommended is registering `AllianceDbContext` as-is — the scaffolded config is complete and battle-tested.)
3. **`UnitOfWork` / `IUnitOfWork`** — constructor takes `AllianceDbContext` instead of `AsiBasecodeDBContext`.
4. **`BaseRepository.Context`** — cast to `AllianceDbContext`.
5. Remove the `#warning` + hardcoded `OnConfiguring` connection string inside `AllianceDbContext.cs` (config now comes from DI).
6. Verify: `dotnet build` / VS build passes; app still boots against `AllianceDB`.

---

## 3. Target architecture (one pattern, replicated per module)

Every CRUD module follows the same vertical slice — build **Equipment** first as the reference implementation, review it, then replicate.

```
Controller (WebApp/Controllers/Admin/)        ← thin: bind, delegate, map, return view/Json
   └─ interface + implementation (Services/)  ← business rules, validation, transactions
        └─ ViewModels (Services/ViewModels/)  ← [Required]/[Range]/[Display] attributes
        └─ Repository (Data/Repositories/)    ← extends BaseRepository; queries only
   └─ AutoMapper map added in Startup.AutoMapper.cs
   └─ DI registrations in Startup.DI.cs (IRepository, IService per module)
```

Conventions to respect:

- `IXxxRepository : BaseRepository` + `XxxRepository` per aggregate — data access only.
- `IXxxService : ServiceBase` + `XxxService` — validation (`ValidationResult`), business rules, multi-table transactions via `UnitOfWork.SaveChangesAsync()`.
- Controllers inherit `ControllerBase<ControllerName>` and inject the service interface (no repository types in controllers).
- Codebook tables (Role, Department, AccountStatus, Location, EquipmentCategory, Condition, BorrowingStatus, ReservationStatus, InspectionStatus, OperationalStatus) are exposed as `SelectList` sources only — no full CRUD UI (admin edits via System Settings where the Figma allows it).
- Row-timeout concurrency: entities with `RowVersion` (User, Equipment, Borrowing, Reservation, balances) → handle `DbUpdateConcurrencyException` with a friendly toastr message.
- Audit: every C/U/D in admin/staff modules writes an `AuditLog` row (actor, action, entity, before/after summary).
- Localized messages through `Resources/Messages` resx; table/screen labels through `Resources/Views` resx.

---

## 4. Module breakdown (Figma → entities → CRUD scope)

### Phase 1 — Equipment Administration (reference slice) · Admin
*Figma: `A - Equipment Administration` (`42:2411`) — see `docs/figma/EquipAdmin.png`*

Figma form fields: Equipment Image (upload), Name*, Asset Code* (unique), Category*, Location*, Total Quantity*, Availability Status*, Condition Classification*, Last/Next Maintenance Date, Description. Delete blocked while active borrowings reference the equipment.

| Item | Content |
|---|---|
| Create | `Equipment` (+ initial `EquipmentInventoryBalance`, `InventoryMovement` IN) |
| Read | List w/ search (asset code/name/category), filters (Category, Location, Status, Condition), paged; Detail view |
| Update | Full edit incl. maintenance dates; recompute balance flags only via movements |
| Delete | Soft/delete-guard: refuse with toastr when `Borrowing`/`Reservation` rows exist (per Figma caption) |
| Tables | `Equipment`, `EquipmentCategory`, `Location`, `Condition` (selectlists + settings CRUD) |
| Files | `EquipmentRepository`, `EquipmentService`, `EquipmentListViewModel`, `EquipmentFormViewModel`, `EquipmentsController` + Index/Detail/Create/Edit views |

### Phase 2 — User Management · Admin
*Figma: `A - User Management` (`42:1726`), `A - User Details` (`42:2142`) — see `docs/figma/UserMgmt.png`*

KPI cards (Total, Students, Faculty, Custodians/Admins, Unverified), role/department/status/verification filters, per-row Edit + Delete, detail view with edit.

- CRUD on `User` (FirstName, LastName, SchoolEmail, AccountIdentifier = school/emp ID, RoleId, DepartmentId, AccountStatusId, contact fields); password hashing = out of scope until auth returns; store `PasswordHash` only when profile update requires it.
- Uniqueness rules: `AccountIdentifier`, `SchoolEmail` (VM-level validation).
- Delete = deactivate (`AccountStatus`) preferred; hard delete blocked if `Borrowing`/`Reservation` history exists — mirrors Equipment guard.

### Phase 3 — Borrower reservation flow · B portal
*Figma: `B - Equipment Availability` (`42:6074`), `B - Reservation Form` (`42:6750`), `B - My Reservations` (`42:6973`), `B - Reservation Details` (`42:7304`) — see `docs/figma/ResForm.png`*

Business rules visible in the form (encode in `ReservationService`):
- Quantity ≤ 3 per borrower, ≤ available units; duration ≤ policy max (7 days default; 21 faculty override); ≤ 3 active transactions; block if borrower has an overdue item; lead time ≥ 1 day; advance booking ≤ 30 days.
- Fields: Quantity*, Pickup Point* (Location), Borrowing Date/Time*, Expected Return Date/Time*, Purpose*, Additional Notes, 2 acknowledgements (required checkboxes).
- **Create** Reservation (StatusId = Pending), **Cancel** (own, while Pending/Approved), **Read** list + details. Borrowers never Update/Delete equipment or others' reservations.

### Phase 4 — Custodian review & release · C portal
*Figma: `C - Reservation Management` (`42:8949`), `C - Reservation Details/Review` (`42:9343`), `C - Equipment Release` (`42:9638`), `C - Active Borrowings` (`42:9978`)*

- **Decision action** on Reservation: Approve (units held) / Reject (VM reason) — status-driven, writes `AuditLog` + `Notification` to borrower.
- **Create `Borrowing`** from an Approved reservation: `TransactionNumber` (system-generated TXN-yyyy-####), ReleasedQuantity ≤ approved, ReleasedByUserId = current custodian, accessory list into notes, condition-at-release echo; decrement `EquipmentInventoryBalance` (Held → Out).
- Active Borrowings list: read-only + overdue highlighting (Due < now and not Returned).

### Phase 5 — Return, inspection, condition · C portal (+ admin visibility)
*Figma: `C - Return Processing` (`42:10384`), `C - Condition Inspection` (`42:10642`) — see `docs/figma/ReturnProc.png`, `docs/figma/Inspection.png`*

- **Create `BorrowingReturn`**: Return DateTime*, QuantityReturned, ReceivedBy*, Return Notes; timeliness computed (On-time / Overdue days / Penalty flag).
- **Create `ConditionInspection`**: outcome per unit (Good / Minor Damage / Damaged / Missing Parts / Lost), damage assessment (affected unit, responsibility, notes, estimated cost), condition photos (file upload → wwwroot/uploads, ≤5 MB, 6 max), post-inspection equipment status transitions.
- Closing inspection completes `Borrowing` → restores inventory balance (Out → Available balance per condition), may spawn `Notification` to borrower, and writes the inspection to equipment condition history.
- **Read** plumbing: Borrowing history lists (admin "Transaction Monitoring" + borrower "My Borrowings/History"), per docs/figma/TransMon.png (KPI cards + tab chips All/Requests/Approvals/Releases/Returns/Overdue/Damaged).

### Phase 6 — Monitoring, reports, audit (read-heavy · Admin)
*Figma: `A - Transaction Monitoring` (`42:2863`), `A - Tracking Reports` (`42:3264`), `A - Audit Logs` (`42:3739`)*

- Read-only + Export (CSV) views. KPI aggregation queries encapsulated in services (`DashboardService`, `ReportService`, `AuditLogService`).
- `AuditLog`, `InventoryMovement`, `BorrowingPolicyVersion` stay **insert-only** (no UI delete/update).

### Phase 7 — System Settings & notifications
*Figma: `A - System Settings` (`42:4084`) — see `docs/figma/SysSettings.png`*

- **Update-only settings screen**: borrowing rules / reservation policy mapped to `BorrowingPolicyVersion` (MaxBorrowDays, MaxUnitsPerBorrower, LeadTime, AdvanceBookingDays, GracePeriod, SuspensionThreshold, FacultyOverrideDays, auto-cancel hours…) — new policy row = new **version**, never UPDATE historical rows.
- **Create/Delete only**: Equipment Categories, Locations chips ("cannot be removed while equipment references it"), Statuses.
- **Read + mark-as-read** for `Notification` (borrower + custodian bars; created by Phase 4/5 actions).
- `A/B/C - Profile & Settings` screens are self-service profile reads/edits — low priority, folds into Phase 2 VMs.

---

## 5. DI + wiring checklist (per new module)

New service/repo pair created → `Startup.DI.cs.ConfigureOtherServices`:
```csharp
this._services.AddScoped<IEquipmentRepository, EquipmentRepository>();
this._services.AddScoped<IEquipmentService, EquipmentService>();
```
New viewmodel mapping → `Startup.AutoMapper.cs` profile:
```csharp
CreateMap<EquipmentFormViewModel, Equipment>().ReverseMap();
CreateMap<Equipment, EquipmentListViewModel>();
```
Views land in `ASI.Basecode.WebApp/Views/<Controller>/` (Index/Detail/Create/Edit.cshtml) — list each view explicitly as part of the module's task.

---

## 6. Sequencing, dependencies & estimates

| # | Phase | Depends on | Est. (sessions) |
|---|---|---|---|
| 0 | Context wiring (Step 0) | — | 0.5 |
| 1 | Equipment CRUD + codebook selects | 0 | 1.5 |
| 2 | User Management CRUD + detail view | 0 | 1 |
| 3 | Borrower reservation flow (create/cancel/read) | 1, 2 | 1.5 |
| 4 | Custodian approve/reject + release (Borrowing create) | 3 | 1.5 |
| 5 | Return + Condition Inspection (BorrowingReturn, ConditionInspection, uploads) | 4 | 2 |
| 6 | Monitoring/Reports/Audit read-only + export | 1,4,5 | 1 |
| 7 | System Settings (policy versioning, category/location mgmt, notifications) | 1 | 1 |
| — | Buffer/regression sweep + end-to-end manual pass | 0–7 | 1 |

Total ≈ **12 focused sessions**. Every phase ends with a green `dotnet build` of the solution (VS 2022 toolchain; standalone `dotnet` CLI is .NET 8 on this machine per AGENTS.md → use VS until a .NET 9 SDK is installed).

---

## 7. Risks / open decisions

1. **Two contexts** — Step 0 must dedupe decisively; leaving `AsiBasecodeDBContext` empty + registered while code targets `AllianceDbContext` is the current trap.
2. **Borrower identity** — auth was stripped (Oct 2026). Until it returns, custodian/borrower flows need a *stand-in current-user* mechanism: a hardcoded/selected demo user injected as `ICurrentUserService` (scoped, reads from session or cookie) — otherwise `ReleasedByUserId`, reservation ownership, notifications can't be populated. Add to Step 0.
3. **Statuses live in DB tables**, not enums — seed script or SQL needed to populate Role/Department/AccountStatus/Location/EquipmentCategory/Condition/status tables before any screen is testable.
4. **File uploads** (equipment image, inspection photos) need `wwwroot/uploads/` + size/type guards + portability note, or external storage later.
5. **Concurrency** — `RowVersion` columns will surface on edits; decide the user-friendly error UX once, reuse everywhere.
6. **Figma token hygiene** — the API key used for this analysis was pasted in chat; **revoke/rotate it in Figma → Settings → Security**. Nothing in this repo stores it.

---

## Progress log

| Date | Phase | Status | Notes |
|---|---|---|---|
| 2026-10-17 | Step 0 — Context wiring | **Done** | `AllianceDbContext` registered from `AllianceConnection` in appsettings; `UnitOfWork`/`BaseRepository` retargeted; hardcoded connection + `#warning` removed from `AllianceDbContext.cs`. |
| 2026-10-17 | Phase 1 — Equipment CRUD reference slice | **Done** | Repository + service + controller + Index/Create/Edit/Details views; AutoMapper maps; DI registrations; audit logs; inventory ledger sync (INITIAL_STOCK/RESTOCK/STOCK_ADJUSTMENT); delete guard; site-wide toastr helper + admin nav. Verified end-to-end over HTTP: create → list/detail → edit → delete, duplicate asset-code rejection, FK-safe cascade on delete. |
| 2026-10-17 | Codebook seed (dev) | **Done** | `sqlcmd` seed: departments, equipment_categories, locations, users (3 demo operators). Roles, account/borrowing/reservation/inspection statuses and conditions were already present. `password_hash` placeholder until auth returns. |

**Slice verified via live run** (`ASI.Basecode.WebApp.exe`, port 5000, `.NET 10 roll-forward` for the smoke test only — keep building/running via VS otherwise). Verification evidence: equipment/balance/movements/audit counts before/after each operation matched design; duplicate asset code rejected with localized message; delete cascaded movements + balance and left a DELETE audit row.

## Appendix — Screen → entity trace (from the Figma canvas)

| Figma node | Screen | Entities touched | Ops |
|---|---|---|---|
| 42:1726 / 42:2142 | A − User Management / User Details | User, Role, Department, AccountStatus | CRUD |
| 42:2411 | A − Equipment Administration | Equipment, EquipmentCategory, Location, Condition | CRUD |
| 42:2863 | A − Transaction Monitoring | Borrowing, Reservation, BorrowingReturn | Read + Export |
| 42:3264 | A − Tracking Reports | all transaction tables | Read + Export |
| 42:3739 | A − Audit Logs | AuditLog | Read |
| 42:4084 | A − System Settings | BorrowingPolicyVersion, codebooks | Update + select codebook Create/Delete |
| 42:6074 / 42:6535 | B − Equipment Availability / Details | Equipment, EquipmentInventoryBalance | Read |
| 42:6750 / 42:6973 / 42:7304 | B − Reservation Form / My Reservations / Details | Reservation, Location | Create, Cancel, Read |
| 42:7538 / 42:7963 / 42:7735 | B − My Borrowings / History / Borrowing Details | Borrowing, BorrowingReturn | Read |
| 42:8949 / 42:9343 | C − Reservation Management / Review | Reservation, Notification | Approve/Reject (action), Read |
| 42:9638 | C − Equipment Release | Borrowing, Reservation, EquipmentInventoryBalance, InventoryMovement | Create (workflow) |
| 42:10384 | C − Return Processing | BorrowingReturn, Borrowing | Create (workflow) |
| 42:10642 | C − Condition Inspection | ConditionInspection, Condition, Equipment, Notification | Create (workflow) |
| 42:11236 / 42:11597 | C − Equipment Management / Detail | Equipment | Read (staff view) |
| 42:8292 / 42:11962 | B/C − Notifications | Notification | Read, mark-as-read |
| 42:4747 … 49:47 | US screens | — | out of scope (auth) |
