# Frontend Redesign Plan — Figma → ASP.NET Core MVC

**Source of truth:** Figma file `Fif2gCnMIWuqEkEYhgAuUv` (page "Initial Deisgn"), 33 screens.
**Backend:** Already complete (Phases 0–7). No backend changes needed — only views, CSS, and layout.
**Strategy:** Replace Bootstrap with a Figma-faithful design system (CSS variables + components). Rebuild each screen's Razor view to match the Figma design. Keep controllers and services untouched.

---

## 1. Design System Foundation (do first)

### 1.1 Design tokens (`wwwroot/css/tokens.css`)
Extracted from Figma — all screens share this palette.

```css
:root {
    /* Primary greens */
    --color-primary: #0b5d3e;
    --color-primary-light: #168a5b;
    --color-primary-dark: #0c6155;
    --color-primary-tint: #eaf5ef;
    --color-primary-tint-2: #eef3f0;
    --color-primary-tint-3: #f4f8f5;

    /* Neutrals */
    --color-text: #17251e;
    --color-text-muted: #2a362f;
    --color-text-soft: #42504a;
    --color-text-gray: #66736c;
    --color-text-lighter: #7c8a83;
    --color-text-faint: #8a968f;
    --color-border: #e9efeb;
    --color-bg: #ffffff;
    --color-bg-light: #f4f8f5;

    /* Semantic */
    --color-info: #245c6e;
    --color-info-tint: #eaf3f7;
    --color-warning: #7a5310;
    --color-warning-bg: #fdf6ea;
    --color-error: #8e2f2f;
    --color-error-bg: #fbeaea;
    --color-success: #168a5b;
    --color-success-bg: #eaf5ef;

    /* Typography */
    --font-family: 'Inter', system-ui, -apple-system, sans-serif;
    --font-size-xs: 10px;
    --font-size-sm: 11px;
    --font-size-base: 12.5px;
    --font-size-md: 13px;
    --font-size-lg: 14px;
    --font-size-xl: 16px;
    --font-size-2xl: 22px;
    --font-size-3xl: 24px;
    --font-size-hero: 38px;

    /* Spacing (from Figma absolute layouts → converted to rem) */
    --space-xs: 4px;
    --space-sm: 8px;
    --space-md: 12px;
    --space-lg: 16px;
    --space-xl: 24px;
    --space-2xl: 32px;
    --space-3xl: 48px;
    --space-4xl: 60px;

    /* Radius */
    --radius-sm: 6px;
    --radius-md: 8px;
    --radius-lg: 10px;
    --radius-xl: 12px;
    --radius-full: 9999px;

    /* Shadows */
    --shadow-sm: 0 1px 3px rgba(0,0,0,0.06);
    --shadow-md: 0 2px 12px rgba(0,0,0,0.06);
    --shadow-lg: 0 4px 24px rgba(0,0,0,0.08);
}
```

### 1.2 Layout shell (`Views/Shared/_Layout.cshtml`)
Replace Bootstrap layout with Figma-faithful shell:
- **Top bar:** logo + "CampusPulse" + subtitle + nav links + profile avatar dropdown
- **Sidebar (admin/custodian):** left rail with icon + label nav items, collapsible
- **Mobile:** bottom nav bar (3-4 items)
- **Content area:** max-width 1280px, centered, with page header (title + subtitle + actions)
- **Footer:** minimal, green background

### 1.3 Component library (`wwwroot/css/components.css`)
Reusable classes matching Figma patterns:
- `.cp-card` — white card with border + radius + shadow
- `.cp-btn-primary`, `.cp-btn-outline`, `.cp-btn-danger`, `.cp-btn-success`
- `.cp-badge` — status badges (success, warning, error, info, neutral)
- `.cp-table` — styled table with hover rows
- `.cp-kpi-card` — metric card (label + large number)
- `.cp-form-group`, `.cp-input`, `.cp-select`, `.cp-textarea`
- `.cp-avatar` — circular avatar with initials
- `.cp-sidebar`, `.cp-sidebar-item`, `.cp-sidebar-item.active`
- `.cp-topbar`, `.cp-topbar-nav`
- `.cp-modal` — confirmation dialogs
- `.cp-toast` — notification toasts
- `.cp-empty-state` — empty state with icon + text
- `.cp-pagination` — pagination controls
- `.cp-filter-bar` — search + dropdown filters

### 1.4 Font loading
Add Inter font via Google Fonts in `_Layout.cshtml` head (or self-host in `wwwroot/lib/fonts/`).

### 1.5 Icon system
Figma uses FontAwesome icons (already loaded via CDN in `_Layout.cshtml`). Keep FontAwesome 6.

---

## 2. Screen-by-screen redesign

Each screen: replace the Razor view's HTML to match Figma layout, colors, typography, spacing. No controller/service changes.

### Wave 1 — Public / Auth (US) — 6 screens
| # | Figma Node | Route | View File | Status |
|---|---|---|---|---|
| 1 | 42:4747 | `/` | `Views/Home/Index.cshtml` | ✅ Done (landing page) |
| 2 | 42:4949 | `/Account/Register` | `Views/Account/Register.cshtml` | Needs create |
| 3 | 42:5061 / 6:3034 | `/Account/Login` | `Views/Account/Login.cshtml` | Needs create |
| 4 | 42:5229 / 49:2 | `/Account/ForgotPassword` | `Views/Account/ForgotPassword.cshtml` | Needs create |
| 5 | 42:5293 / 49:47 | `/Account/ResetPassword` | `Views/Account/ResetPassword.cshtml` | Needs create |
| 6 | 6:3207 | `/Account/PasswordReset` | `Views/Account/PasswordReset.cshtml` | Needs create |

**Note:** Auth was stripped per AGENTS.md. These views are placeholders until auth returns. Build them as static Figma-faithful pages with form inputs (non-functional).

### Wave 2 — Borrower portal (B) — 11 screens
| # | Figma Node | Route | View File |
|---|---|---|---|
| 7 | 42:5379 | `/Borrower/Dashboard` | `Views/Borrower/Dashboard.cshtml` |
| 8 | 42:5746 | `/Borrower/Profile` | `Views/Borrower/Profile.cshtml` |
| 9 | 42:6074 | `/Reservations/Available` | `Views/Reservations/Available.cshtml` |
| 10 | 42:6535 | `/Reservations/Details` (equipment) | `Views/Reservations/EquipmentDetails.cshtml` |
| 11 | 42:6750 | `/Reservations/New` | `Views/Reservations/New.cshtml` |
| 12 | 42:6973 | `/Reservations/Mine` | `Views/Reservations/Mine.cshtml` |
| 13 | 42:7304 | `/Reservations/Details` | `Views/Reservations/Details.cshtml` |
| 14 | 42:7538 | `/Borrower/Borrowings` | `Views/Borrower/Borrowings.cshtml` |
| 15 | 42:7735 | `/Borrower/BorrowingDetails` | `Views/Borrower/BorrowingDetails.cshtml` |
| 16 | 42:7963 | `/Borrower/BorrowingHistory` | `Views/Borrower/BorrowingHistory.cshtml` |
| 17 | 42:8292 | `/Notifications/Index` | `Views/Notifications/Index.cshtml` |

### Wave 3 — Custodian portal (C) — 11 screens
| # | Figma Node | Route | View File |
|---|---|---|---|
| 18 | 42:8542 | `/Custodian/Dashboard` | `Views/Custodian/Dashboard.cshtml` |
| 19 | 42:8949 | `/Custodian/Reservations` | `Views/Custodian/Reservations.cshtml` |
| 20 | 42:9343 | `/Custodian/Review` | `Views/Custodian/Review.cshtml` |
| 21 | 42:9638 | `/Custodian/Release` | `Views/Custodian/Release.cshtml` |
| 22 | 42:9978 | `/Custodian/Borrowings` | `Views/Custodian/Borrowings.cshtml` |
| 23 | 42:10384 | `/Returns/Create` | `Views/Returns/Create.cshtml` |
| 24 | 42:10642 | `/Returns/Inspect` | `Views/Returns/Inspect.cshtml` |
| 25 | 42:10896 | `/Custodian/Profile` | `Views/Custodian/Profile.cshtml` |
| 26 | 42:11236 | `/Custodian/Equipment` | `Views/Custodian/Equipment.cshtml` |
| 27 | 42:11597 | `/Custodian/EquipmentDetails` | `Views/Custodian/EquipmentDetails.cshtml` |
| 28 | 42:11962 | `/Custodian/Notifications` | `Views/Custodian/Notifications.cshtml` |

### Wave 4 — Admin portal (A) — 9 screens
| # | Figma Node | Route | View File |
|---|---|---|---|
| 29 | 42:1319 | `/Admin/Dashboard` | `Views/Admin/Dashboard.cshtml` |
| 30 | 42:1726 | `/Users/Index` | `Views/Users/Index.cshtml` |
| 31 | 42:2142 | `/Users/Details` | `Views/Users/Details.cshtml` |
| 32 | 42:2411 | `/Equipment/Index` | `Views/Equipment/Index.cshtml` |
| 33 | 42:2863 | `/Monitoring/Transactions` | `Views/Monitoring/Transactions.cshtml` |
| 34 | 42:3264 | `/Monitoring/Reports` | `Views/Monitoring/Reports.cshtml` |
| 35 | 42:3739 | `/Monitoring/AuditLogs` | `Views/Monitoring/AuditLogs.cshtml` |
| 36 | 42:4084 | `/Settings/Index` | `Views/Settings/Index.cshtml` |
| 37 | 42:4402 | `/Admin/Profile` | `Views/Admin/Profile.cshtml` |

---

## 3. Execution order

### Phase A — Design system (1 session)
1. Create `wwwroot/css/tokens.css` with all CSS variables
2. Create `wwwroot/css/components.css` with reusable component classes
3. Rewrite `Views/Shared/_Layout.cshtml` with Figma-faithful shell (top bar + sidebar + mobile nav)
4. Add Inter font loading
5. Verify: landing page still renders, all routes still 200

### Phase B — Public/Auth screens (1 session)
6. Rebuild Login, Register, Forgot/Reset Password views from Figma
7. These are static (auth stripped) — form inputs only, no functionality

### Phase C — Borrower portal (2 sessions)
8. Rebuild Dashboard, Equipment Availability, Equipment Details
9. Rebuild Reservation Form, My Reservations, Reservation Details
10. Rebuild My Borrowings, Borrowing History, Notifications, Profile

### Phase D — Custodian portal (2 sessions)
11. Rebuild Dashboard, Reservation Management, Review, Release
12. Rebuild Active Borrowings, Return Processing, Condition Inspection, Equipment Management, Notifications, Profile

### Phase E — Admin portal (2 sessions)
13. Rebuild Dashboard, User Management, User Details
14. Rebuild Equipment Administration, Transaction Monitoring, Tracking Reports, Audit Logs, System Settings, Profile

### Phase F — Polish (1 session)
15. Responsive testing (1280/768/375 breakpoints)
16. Cross-browser check (Chrome, Edge, Firefox)
17. Accessibility audit (keyboard nav, ARIA labels, contrast)
18. Remove Bootstrap dependency (if fully replaced)

**Total: ~9 sessions**

---

## 4. Rules for each screen redesign

1. **No backend changes** — controllers, services, repositories stay as-is. Only views + CSS.
2. **Preserve all functionality** — forms must still post to the same actions with the same field names.
3. **Use design tokens** — all colors, fonts, spacing must come from CSS variables, never hardcoded hex.
4. **Responsive** — every screen must work at 1280px, 768px, and 375px.
5. **Commit per screen** — each screen gets its own commit so changes are reviewable and reversible.
6. **Verify after each** — build + HTTP smoke test (route returns 200, no runtime errors).
7. **Match Figma** — use the rendered PNGs in `docs/figma/` as visual reference. Pixel-perfect is the goal but 95% fidelity is acceptable for v1.

---

## 5. Figma MCP access

The Figma MCP server is registered in `~/.config/opencode/opencode.json`. On the next OpenCode launch, a browser OAuth prompt will appear. After authorization, I can use `tools.figma.*` for richer component-level access (auto-layout, constraints, exact spacing, design tokens as variables).

Until then, I'm using the Figma REST API with the provided token for frame rendering and node tree extraction.

---

## 6. What stays vs. what changes

### Stays (no changes)
- All `Controllers/*.cs` (business logic routing)
- All `Services/**/*.cs` (business logic, view models)
- All `Data/**/*.cs` (repositories, DbContext, entities)
- All `Resources/**/*` (resx, constants, enums)
- `Startup.*.cs` (DI, AutoMapper, logging)
- `Program.cs`, `appsettings.json`

### Changes
- `Views/**/*.cshtml` — full rewrite to Figma-faithful markup
- `Views/Shared/_Layout.cshtml` — new design system shell
- `Views/Shared/_Header.cshtml` — new top bar + nav
- `wwwroot/css/*.css` — new design system (tokens, components, layout)
- `wwwroot/css/site.css` — may be replaced entirely
- `wwwroot/css/landing.css` — already created, fold into tokens.css

### Removed (eventually)
- Bootstrap dependency (`wwwroot/lib/bootstrap/`) — once all screens are converted
- jQuery (if no longer needed for form validation — keep for now)