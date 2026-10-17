# Figma Design Reference — Alliance Group 5 (CampusPulse)

Source: Figma file `Fif2gCnMIWuqEkEYhgAuUv`, page "Initial Deisgn"
Rendered screens: `docs/figma/*.png`
Connected via Figma REST API (token-based). Figma MCP server registered in `~/.config/opencode/opencode.json` (requires OAuth consent on next OpenCode launch).

## Design tokens (extracted from A - Dashboard)

### Color palette (36 unique colors)
| Hex | Usage hint |
|---|---|
| `#0b5d3e` | Primary dark green (sidebar/header) |
| `#168a5b` | Primary green (buttons, accents) |
| `#0c6155` | Teal-green (secondary accent) |
| `#245c6e` | Blue-teal (info) |
| `#17251e` | Near-black text on green |
| `#2a362f` | Dark text |
| `#3e514a` / `#42504a` | Muted dark text |
| `#66736c` / `#7c8a83` / `#7e8c85` / `#8a968f` / `#93a09a` / `#9aa6a0` / `#a6b1ab` | Gray text variants |
| `#a9d8c2` | Light green (highlights) |
| `#e2f2ee` / `#eaf5ef` / `#edf2ef` / `#edf3ee` / `#eef2f0` / `#eef3f0` / `#f0f2f1` / `#f4f8f5` / `#f7faf8` | Light green backgrounds |
| `#e6f1f5` / `#eaf3f7` | Light blue backgrounds |
| `#e79b9b` / `#e8c87e` | Warning/error accent |
| `#8e2f2f` | Dark red (error text) |
| `#7a5310` | Dark amber (warning text) |
| `#eaf3f7` | Light blue |
| `#fbeaea` / `#fceeee` | Light red backgrounds |
| `#fdf3e1` / `#fdf6ea` | Light amber backgrounds |
| `#ffffff` | White (cards, surfaces) |

### Typography
- **Font family:** Inter (all weights)
- **Weights used:** 400 (regular), 500 (medium), 600 (semibold), 700 (bold)
- **Size range:** 9.5px – 24px (dense UI, 10–13px body text)
- **Line heights:** 10.5–24px (tight, compact layout)

### Layout
- **Canvas width:** 1280px (all screens)
- **No auto-layout detected** — screens use absolute positioning (Figma frames, not responsive flex)
- **Sidebar:** left rail (admin/custodian screens)
- **Top bar:** header with logo + profile avatar

## Screen inventory (33 frames)

### A — Administrator portal
| Node | Screen | Entities |
|---|---|---|
| 42:1319 | A - Dashboard | KPIs, charts |
| 42:1726 | A - User Management | User CRUD |
| 42:2142 | A - User Details | User detail |
| 42:2411 | A - Equipment Administration | Equipment CRUD |
| 42:2863 | A - Transaction Monitoring | Borrowing/Reservation read |
| 42:3264 | A - Tracking Reports | Reports |
| 42:3739 | A - Audit Logs | AuditLog read |
| 42:4084 | A - System Settings | Policy, codebooks |
| 42:4402 | A - Profile & Settings | Profile |

### B — Borrower portal
| Node | Screen |
|---|---|
| 42:5379 | B - Dashboard |
| 42:5746 | B - Profile & Settings |
| 42:6074 | B - Equipment Availability |
| 42:6535 | B - Equipment Details |
| 42:6750 | B - Reservation Form |
| 42:6973 | B - My Reservations |
| 42:7304 | B - Reservation Details |
| 42:7538 | B - My Borrowings |
| 42:7735 | B - Borrowing Details |
| 42:7963 | B - Borrowing History |
| 42:8292 | B - Notifications |

### C — Custodian portal
| Node | Screen |
|---|---|
| 42:8542 | C - Dashboard |
| 42:8949 | C - Reservation Management |
| 42:9343 | C - Reservation Details / Review |
| 42:9638 | C - Equipment Release |
| 42:9978 | C - Active Borrowings |
| 42:10384 | C - Return Processing |
| 42:10642 | C - Condition Inspection |
| 42:10896 | C - Profile & Settings |
| 42:11236 | C - Equipment Management |
| 42:11597 | C - Equipment Detail |
| 42:11962 | C - Notifications |

### US — Universal (public/auth)
| Node | Screen |
|---|---|
| 42:4747 | US - Landing Page |
| 42:4949 / 42:5061 / 6:3034 | US - Registration / Login |
| 42:5229 / 49:2 | US - Forgot Password |
| 42:5293 / 49:47 | US - Reset Password |
| 6:3207 | US - Password Reset |

### Other
| Node | Screen |
|---|---|
| 13:1780 | Checklist (task checklist) |

## Rendered screenshots
All 8 key screens are saved in `docs/figma/`:
- `UserMgmt.png`, `EquipAdmin.png`, `TransMon.png`, `ResForm.png`
- `ReturnProc.png`, `Inspection.png`, `SysSettings.png`, `Checklist.png`

## Next steps
To redesign the frontend from these designs, the recommended approach:
1. **Keep the backend** (Data/Services) — it's verified and complete.
2. **Replace Razor views** one screen at a time with Figma-faithful markup (Inter font, green palette, 1280px canvas, absolute-positioned layout converted to CSS).
3. **Start with one pilot screen** (e.g., A - Dashboard or A - Equipment Administration) to establish the design system (CSS variables, layout shell, component patterns), then roll out to the rest.

The Figma MCP (`https://mcp.figma.com/mcp`) is registered but requires OAuth browser consent on the next OpenCode launch for richer component-level access.