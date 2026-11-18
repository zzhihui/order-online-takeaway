# Order Online — Restaurant Takeaway Ordering

A multi-tenant platform that lets restaurants take online takeaway and delivery orders, manage menus and opening hours, notify the kitchen, and accept card payments.

This is a 2015–2017 codebase, published as a portfolio snapshot. Secrets, certificates, local databases and credentials have been removed or replaced with `REPLACE_ME` placeholders. The project is not production-ready as-is.

## Licence

All rights reserved © John Zhao. The source is shown for portfolio purposes only; no permission is granted to use it commercially. See [LICENSE](LICENSE).


## What problem it solves

Small independent restaurants (the original deployments were mainly Chinese, Japanese and pan-Asian takeaways in Ireland) needed their own branded ordering website, a way for staff to receive and process orders, and optional mobile apps — without each shop commissioning a one-off site. This system provides a shared backend with a separate storefront per restaurant.

## Features

- Per-restaurant branded ordering websites (MVC Areas; this snapshot includes demo storefronts only)
- Menu with categories, sub-items, optional extras and special offers
- Collection vs delivery, delivery areas and delivery charges, minimum order
- Opening hours, including special-day hours
- Customer accounts, order history and SMS/email notifications (Twilio / SMTP)
- Stripe card payments
- Kitchen / shop admin tools (WPF desktop apps, notification pop-ups, Android admin)
- Customer mobile app (Cordova / Android) and caller-ID helper for incoming phone orders
- Discount rules, shop news and basic licensing
- Admin / support areas for the platform operator

## Architecture

The solution is a large n-tier .NET Framework system with several clients talking to a shared service and SQL Server database:

```
Takeway.Web                  ASP.NET MVC 5 + Web API — customer storefronts and platform admin
Takeaway.Service             Application services and transformers
Takeaway.Service.Contract    Service contracts / DTOs
Takeaway.BusinessEngine      Domain logic (orders, shops, payments, SMS, email)
Takeaway.Data*               Entity Framework 6 context, entities, repositories
Core.Common                  Shared utilities
Takeaway.Admin.Wpf           Desktop admin client
Takeaway.Notification.Wpf    Desktop order-notification client
Takeaway.Admin.Monitor       Monitoring utility
Takeaway.Admin.Service       Admin service host
Takeway.Mobile.*             Xamarin Android admin/client and shared mobile core
App.App / App.Payment        Cordova (Ionic-era) customer and payment apps
callerId                     Caller-ID integration for phone orders
```

Multi-tenancy is physical MVC Areas: each restaurant has its own area with CSS, images and sometimes a menu PDF, while orders and menus are shop-scoped in a shared SQL Server database (`takeaway_online` / `shop_fusion` in the original environments).

## Tech stack

| Layer | Technology |
| --- | --- |
| Runtime | .NET Framework 4.5 (a few projects target 4.0 or 2.0) |
| Web | ASP.NET MVC 5, ASP.NET Web API 2, Razor |
| Desktop | WPF |
| Mobile | Xamarin.Android and Apache Cordova |
| Data | Entity Framework 6.1, SQL Server, Dapper (caller-ID helper) |
| Payments | Stripe.net |
| SMS | Twilio 4.7 |
| Email | SMTP / SendGrid |
| Maps | Google Maps API library (no API key is bundled) |
| Tests | MSTest / integration and unit test projects |
| IDE | Visual Studio 2015 or 2017 with Xamarin/Cordova workloads if you build the mobile apps |

## Project structure

```
├── resources/                 # Design mockups (Photoshop source files omitted)
├── src/
│   ├── Takeaway.sln
│   ├── Takeway.Web/           # Web storefronts (Areas per restaurant) + API
│   ├── Takeaway.Service*/     # Services, contracts, tests
│   ├── Takeaway.BusinessEngine/
│   ├── Takeaway.Data*/
│   ├── Takeaway.Admin.*/      # WPF admin, monitor, utilities
│   ├── Takeway.Mobile.*/      # Android + shared core
│   ├── App.App/               # Cordova customer app
│   ├── App.Payment/           # Cordova payment app
│   └── Exlib/                 # Helper binaries (signing keystores are not included)
```

NuGet packages and `bower_components` are not committed. Restore NuGet packages to `src/packages/` before building.

## How to build and run

1. Install Visual Studio 2017 or later with the **ASP.NET and web development** workload and the **.NET Framework 4.5 targeting pack**. Use a local SQL Server instance. Mobile/Cordova projects need the corresponding workloads and are optional if you only want the web app.
2. Open `src/Takeaway.sln` and restore NuGet packages (`nuget restore src/Takeaway.sln`).
3. Replace every `REPLACE_ME` in `src/Takeway.Web/Web.config`, environment transform files, and other `App.config` files you will run:
   - SQL Server connection string (local `sa` password, or a new Azure SQL account — do not reuse the original production server)
   - `ShopAuthKey`
   - Stripe test key and SMTP credentials in `Takeaway.BusinessEngine/App.config`
   - Encryption passphrases and RSA key XML in `Core.Common` if authentication tokens must work
4. Create an empty database (default names in config include `takeaway_online`) and point the connection string at it. This snapshot does not include a full schema migration pack; you will need to derive the schema from the EF mappings or a fresh database bootstrap.
5. Set `Takeway.Web` as the startup project and run. Demo storefronts live under the `Demo`, `Demo2`, `DemoMulti` and `DemoChinese` areas.

Android release builds require you to create a new keystore; the original keystores were removed and must not be reused from backups.

## Status / roadmap

This is an archived snapshot of a multi-restaurant takeaway product used in Ireland around 2015–2017. It is published so the architecture and domain model can be reviewed, not as a service to operate.

Please note:

- Real-client restaurant storefronts (branded MVC Areas, logos, photos and menus for the original Irish shop deployments) have been removed. The web project now keeps only the `Demo`, `Demo2`, `DemoMulti` and `DemoChinese` storefronts, plus `IndaSoftAdmin` and `HelpPage`. Per-restaurant custom-domain rewrite rules were removed from the production web.config transform. The solution should still parse; those shops will not be present at runtime.
- Third-party binaries with unclear licences were removed from `src/Exlib/` and `src/callerId/` (ConfuserEx, QNV caller-ID DLLs `qnviccub` / `qnvfiletrans` / `bridge`, `DesktopAlert.dll`, `RLRecoder.dll`, SqlConverter zips, and vendored Xamarin.Android.Support assemblies). The WPF notification toast, caller-ID helpers (`callerId`, `pstndemo`) and Android projects will not build until you obtain those libraries separately. Xamarin Android Support packages can be restored via NuGet / the Xamarin workload. The web application does not need them.
- The stack is dated (MVC 5, WPF, Cordova, .NET Framework).
- All production credentials found in the original tree were stripped; assume any historical key is compromised and must be rotated if the underlying account still exists.

A sensible modernisation path is a single multi-tenant ASP.NET Core API, a shared storefront theme with configuration-based branding (no one-folder-per-shop), and mobile clients rebuilt on a current stack. Keep the domain model — menus, modifiers, delivery zones, opening hours and order states — as it is the valuable part of this codebase.
