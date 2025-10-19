# DỰ ÁN HOÀNG NGỌC - TÀI LIỆU TRIỂN KHAI ORCHARD CORE 2.2.1

## 📁 CẤU TRÚC DỰ ÁN CHUẨN ORCHARD CORE 2.2.1

### **📁 CẤU TRÚC TỔNG QUAN**

```
📁 HoangNgocWebsite/                    ← Solution root
├── 📄 HoangNgocWebsite.sln             ← Solution file
├── 📄 global.json                      ← .NET SDK version lock
├── 📄 Directory.Build.props            ← Shared properties
├── 📄 .gitignore                       ← Git ignore
├── 📄 .editorconfig                    ← Editor configuration
├── 📄 README.md                        ← Documentation
├── 📄 docker-compose.yml               ← Docker (optional)
│
├── 📁 HoangNgocWebsite/                ← Main CMS project
│   ├── 📄 HoangNgocWebsite.csproj      ← Project file (2.2.1)
│   ├── 📄 Program.cs                   ← Application entry point
│   ├── 📄 appsettings.json             ← Production settings
│   ├── 📄 appsettings.Development.json ← Development settings
│   ├── 📄 NLog.config                  ← Logging configuration
│   ├── 📄 web.config                   ← IIS configuration
│   │
│   ├── 📁 App_Data/                    ← Orchard data
│   │   ├── 📁 Sites/                   ← Site configurations
│   │   ├── 📁 Localization/            ← Localization files
│   │   ├── 📁 Logs/                    ← Application logs
│   │   └── 📁 DataProtection-Keys/     ← Data protection keys
│   │
│   └── 📁 wwwroot/                     ← Static web files
│       ├── 📄 favicon.ico              ← Site favicon
│       ├── 📁 css/                     ← Custom CSS
│       ├── 📁 js/                      ← Custom JavaScript
│       ├── 📁 images/                  ← Site images
│       └── 📁 media/                   ← Media files
│
├── 📁 Modules/                         ← Modules folder
│   │
│   ├── 📁 HoangNgoc.Users/             ← User Management & Wallet module
│   │   ├── 📄 HoangNgoc.Users.csproj   ← User module project
│   │   ├── 📄 Manifest.cs              ← User module manifest
│   │   ├── 📄 Startup.cs               ← User module startup
│   │   ├── 📄 Migrations.cs            ← User database migrations
│   │   │
│   │   ├── 📁 Services/                ← User services
│   │   │   ├── 📄 UserRegistrationService.cs ← Registration service
│   │   │   ├── 📄 UserProfileService.cs ← Profile management
│   │   │   ├── 📄 WalletService.cs     ← Wallet management
│   │   │   └── 📄 TransactionService.cs ← Transaction management
│   │   │
│   │   ├── 📁 Controllers/             ← User controllers
│   │   │   ├── 📄 AccountController.cs ← Login/Register/Profile
│   │   │   ├── 📄 WalletController.cs  ← Wallet management
│   │   │   └── 📄 UserDashboardController.cs ← User dashboard
│   │   │
│   │   ├── 📁 Models/                  ← User models
│   │   │   ├── 📄 UserProfilePart.cs   ← User profile part
│   │   │   ├── 📄 WalletPart.cs        ← User wallet part
│   │   │   ├── 📄 TransactionModel.cs  ← Transaction model
│   │   │   └── 📄 TopUpRequestModel.cs ← Top-up request model
│   │   │
│   │   ├── 📁 ViewModels/              ← User view models
│   │   │   ├── 📄 RegisterViewModel.cs ← Registration form
│   │   │   ├── 📄 LoginViewModel.cs    ← Login form
│   │   │   ├── 📄 ProfileViewModel.cs  ← Profile form
│   │   │   ├── 📄 WalletViewModel.cs   ← Wallet display
│   │   │   └── 📄 TopUpViewModel.cs    ← Top-up form
│   │   │
│   │   ├── 📁 Drivers/                 ← Content part drivers
│   │   │   ├── 📄 UserProfilePartDriver.cs ← Profile part driver
│   │   │   └── 📄 WalletPartDriver.cs  ← Wallet part driver
│   │   │
│   │   ├── 📁 Views/                   ← User views
│   │   │   ├── 📁 Account/             ← Account views
│   │   │   │   ├── 📄 Register.cshtml  ← Registration page
│   │   │   │   ├── 📄 Login.cshtml     ← Login page
│   │   │   │   ├── 📄 Profile.cshtml   ← Profile page
│   │   │   │   └── 📄 Dashboard.cshtml ← User dashboard
│   │   │   ├── 📁 Wallet/              ← Wallet views
│   │   │   │   ├── 📄 Dashboard.cshtml ← Wallet dashboard
│   │   │   │   ├── 📄 TopUp.cshtml     ← Top-up form
│   │   │   │   ├── 📄 History.cshtml   ← Transaction history
│   │   │   │   └── 📄 Balance.cshtml   ← Balance display
│   │   │   └── 📁 Parts/
│   │   │       ├── 📄 UserProfile.cshtml ← Profile display
│   │   │       └── 📄 WalletWidget.cshtml ← Wallet widget
│   │   │
│   │   ├── 📁 Workflows/               ← User workflows
│   │   │   ├── 📄 UserRegistrationActivity.cs ← Registration workflow
│   │   │   ├── 📄 WalletTopUpActivity.cs ← Top-up workflow
│   │   │   └── 📄 TransactionActivity.cs ← Transaction workflow
│   │   │
│   │   ├── 📁 Permissions/             ← User permissions
│   │   │   └── 📄 UserPermissions.cs   ← User management permissions
│   │   │
│   │   ├── 📁 Indexes/                 ← Database indexes
│   │   │   ├── 📄 UserProfileIndex.cs  ← User profile index
│   │   │   ├── 📄 WalletIndex.cs       ← Wallet index
│   │   │   └── 📄 TransactionIndex.cs  ← Transaction index
│   │   │
│   │   ├── 📁 GraphQL/                 ← GraphQL support
│   │   │   ├── 📄 UserQueries.cs       ← User GraphQL queries
│   │   │   └── 📄 WalletMutations.cs   ← Wallet GraphQL mutations
│   │   │
│   │   └── 📁 Recipes/                 ← User setup
│   │       ├── 📄 user-setup.recipe.json ← User module setup
│   │       └── 📄 user-roles.recipe.json ← User roles setup
│   │
│   ├── 📁 HoangNgoc.Core/              ← Core module
│   │   ├── 📄 HoangNgoc.Core.csproj    ← Core module project
│   │   ├── 📄 Manifest.cs              ← Core manifest
│   │   ├── 📄 Startup.cs               ← Core startup
│   │   │
│   │   ├── 📁 Abstractions/            ← Shared interfaces
│   │   │   ├── 📄 IPaymentService.cs   ← Payment service interface
│   │   │   ├── 📄 ICourseService.cs    ← Course service interface
│   │   │   └── 📄 IApplicationService.cs ← Application service interface
│   │   │
│   │   ├── 📁 Models/                  ← Shared models
│   │   │   ├── 📄 PaymentResult.cs     ← Payment result model
│   │   │   ├── 📄 CourseEnrollment.cs  ← Course enrollment model
│   │   │   └── 📄 ApplicationUsage.cs  ← Application usage model
│   │   │
│   │   ├── 📁 Permissions/             ← Shared permissions
│   │   │   └── 📄 CorePermissions.cs   ← Core permissions
│   │   │
│   │   ├── 📁 Extensions/              ← Extension methods
│   │   │   └── 📄 ContentExtensions.cs ← Content helpers
│   │   │
│   │   └── 📁 Recipes/                 ← Setup recipes
│   │       ├── 📄 HoangNgocSite.recipe.json    ← Custom site recipe
│   │       └── 📄 HoangNgocContent.recipe.json ← Content setup recipe
│   │
│   ├── 📁 HoangNgoc.Payment/           ← Payment module
│   │   ├── 📄 HoangNgoc.Payment.csproj ← Module project file
│   │   ├── 📄 Manifest.cs              ← Module manifest
│   │   ├── 📄 Startup.cs               ← Module startup
│   │   ├── 📄 Migrations.cs            ← Database migrations
│   │   │
│   │   ├── 📁 Services/                ← Gateway Services
│   │   │   ├── 📄 VNPayService.cs      ← VNPay integration
│   │   │   ├── 📄 MoMoService.cs       ← MoMo integration
│   │   │   ├── 📄 ZaloPayService.cs    ← ZaloPay integration
│   │   │   └── 📄 WalletService.cs     ← Wallet management
│   │   │
│   │   ├── 📁 Controllers/             ← Payment controllers
│   │   │   ├── 📄 PaymentController.cs ← Payment processing
│   │   │   └── 📄 CallbackController.cs ← Gateway callbacks
│   │   │
│   │   ├── 📁 ViewModels/              ← View models
│   │   │   ├── 📄 PaymentViewModel.cs  ← Payment forms
│   │   │   └── 📄 WalletViewModel.cs   ← Wallet display
│   │   │
│   │   ├── 📁 Drivers/                 ← ContentPart drivers
│   │   │   └── 📄 WalletPartDriver.cs  ← Wallet part driver
│   │   │
│   │   ├── 📁 Parts/                   ← Content parts
│   │   │   └── 📄 WalletPart.cs        ← Wallet content part
│   │   │
│   │   ├── 📁 Handlers/                ← Content handlers
│   │   │   └── 📄 PaymentContentHandler.cs ← Payment content handler
│   │   │
│   │   ├── 📁 GraphQL/                 ← GraphQL support
│   │   │   ├── 📄 PaymentQueries.cs    ← Payment GraphQL queries
│   │   │   └── 📄 PaymentMutations.cs  ← Payment GraphQL mutations
│   │   │
│   │   ├── 📄 Placement.json           ← Shape placement
│   │   ├── 📄 Module.png               ← Module icon
│   │   │
│   │   ├── 📁 Indexes/                 ← Database indexes
│   │   │   ├── 📄 PaymentIndex.cs      ← Payment index
│   │   │   └── 📄 WalletIndex.cs       ← Wallet index
│   │   │
│   │   ├── 📁 Workflows/               ← Workflow activities
│   │   │   ├── 📄 PaymentActivity.cs   ← Payment workflow activity
│   │   │   └── 📄 WalletActivity.cs    ← Wallet workflow activity
│   │   │
│   │   ├── 📁 Liquid/                  ← Liquid filters
│   │   │   ├── 📄 PaymentFilters.cs    ← Payment Liquid filters
│   │   │   └── 📄 WalletFilters.cs     ← Wallet Liquid filters
│   │   │
│   │   ├── 📁 Permissions/             ← Module permissions
│   │   │   └── 📄 PaymentPermissions.cs
│   │   │
│   │   ├── 📁 Views/                   ← Payment views
│   │   │   ├── 📁 Payment/             ← Admin views
│   │   │   │   ├── 📄 Index.cshtml     ← Payment management
│   │   │   │   ├── 📄 Create.cshtml    ← Create payment
│   │   │   │   └── 📄 Edit.cshtml      ← Edit payment
│   │   │   ├── 📁 PaymentFrontend/     ← Frontend views
│   │   │   │   ├── 📄 Index.cshtml     ← Payment form
│   │   │   │   ├── 📄 Success.cshtml   ← Success page
│   │   │   │   └── 📄 Failed.cshtml    ← Failed page
│   │   │   └── 📁 Parts/
│   │   │       └── 📄 Wallet.cshtml    ← Wallet display
│   │   │
│   │   ├── 📁 Recipes/                 ← Module setup
│   │   │   ├── 📄 payment-setup.recipe.json ← Payment module setup
│   │   │   └── 📄 payment-contenttypes.recipe.json ← Payment ContentTypes setup
│   │   │
│   │   └── 📁 Models/                  ← Data models
│   │       ├── 📄 PaymentPart.cs       ← Payment content part
│   │       ├── 📄 WalletPart.cs        ← Wallet content part
│   │       └── 📄 TransactionModel.cs  ← Transaction model
│   │
│   ├── 📁 HoangNgoc.Training/          ← Training module
│   │   ├── 📄 HoangNgoc.Training.csproj ← Module project file
│   │   ├── 📄 Manifest.cs              ← Module manifest
│   │   ├── 📄 Startup.cs               ← Module startup
│   │   ├── 📄 Migrations.cs            ← Database migrations
│   │   │
│   │   ├── 📁 Services/                ← Training services
│   │   │   ├── 📄 CourseService.cs     ← Course management
│   │   │   ├── 📄 LessonService.cs     ← Lesson management
│   │   │   ├── 📄 ProgressService.cs   ← Progress tracking
│   │   │   └── 📄 CertificateService.cs ← Certificate generation
│   │   │
│   │   ├── 📁 Controllers/             ← Training controllers
│   │   │   ├── 📄 CourseController.cs  ← Course management
│   │   │   ├── 📄 LessonController.cs  ← Lesson delivery
│   │   │   └── 📄 CertificateController.cs ← Certificate management
│   │   │
│   │   ├── 📁 Parts/                   ← Content parts
│   │   │   ├── 📄 CoursePart.cs        ← Course content part
│   │   │   ├── 📄 LessonPart.cs        ← Lesson content part
│   │   │   └── 📄 ProgressPart.cs      ← Progress content part
│   │   │
│   │   ├── 📁 Drivers/                 ← ContentPart drivers
│   │   │   ├── 📄 CoursePartDriver.cs  ← Course part driver
│   │   │   ├── 📄 LessonPartDriver.cs  ← Lesson part driver
│   │   │   └── 📄 ProgressPartDriver.cs ← Progress part driver
│   │   │
│   │   ├── 📁 ViewModels/              ← View models
│   │   │   ├── 📄 CourseViewModel.cs   ← Course forms
│   │   │   ├── 📄 LessonViewModel.cs   ← Lesson forms
│   │   │   └── 📄 ProgressViewModel.cs ← Progress display
│   │   │
│   │   ├── 📁 Views/                   ← Training views
│   │   │   ├── 📁 Course/              ← Course views
│   │   │   │   ├── 📄 Index.cshtml     ← Course list
│   │   │   │   ├── 📄 Details.cshtml   ← Course details
│   │   │   │   └── 📄 Enroll.cshtml    ← Enrollment form
│   │   │   ├── 📁 Lesson/              ← Lesson views
│   │   │   │   ├── 📄 Index.cshtml     ← Lesson list
│   │   │   │   └── 📄 Player.cshtml    ← Lesson player
│   │   │   └── 📁 Parts/
│   │   │       ├── 📄 Course.cshtml    ← Course display
│   │   │       ├── 📄 Lesson.cshtml    ← Lesson display
│   │   │       └── 📄 Progress.cshtml  ← Progress display
│   │   │
│   │   ├── 📁 GraphQL/                 ← GraphQL support
│   │   │   ├── 📄 CourseQueries.cs     ← Course GraphQL queries
│   │   │   └── 📄 ProgressMutations.cs ← Progress GraphQL mutations
│   │   │
│   │   ├── 📁 Workflows/               ← Training workflows
│   │   │   ├── 📄 EnrollmentActivity.cs ← Enrollment workflow
│   │   │   └── 📄 ProgressActivity.cs  ← Progress workflow
│   │   │
│   │   ├── 📁 Liquid/                  ← Liquid filters
│   │   │   ├── 📄 CourseFilters.cs     ← Course Liquid filters
│   │   │   └── 📄 ProgressFilters.cs   ← Progress Liquid filters
│   │   │
│   │   ├── 📁 Permissions/             ← Module permissions
│   │   │   └── 📄 TrainingPermissions.cs
│   │   │
│   │   └── 📁 Recipes/                 ← Module setup
│   │       ├── 📄 training-setup.recipe.json ← Training module setup
│   │       └── 📄 training-contenttypes.recipe.json ← Training ContentTypes setup
│   │
│   ├── 📁 HoangNgoc.Application/       ← Application module
│   │   ├── 📄 HoangNgoc.Application.csproj ← Module project file
│   │   ├── 📄 Manifest.cs              ← Module manifest
│   │   ├── 📄 Startup.cs               ← Module startup
│   │   ├── 📄 Migrations.cs            ← Database migrations
│   │   │
│   │   ├── 📁 Services/                ← Application services
│   │   │   ├── 📄 ApplicationService.cs ← Application management
│   │   │   ├── 📄 AccessService.cs     ← Access control
│   │   │   └── 📄 UsageService.cs      ← Usage tracking
│   │   │
│   │   ├── 📁 Controllers/             ← Application controllers
│   │   │   ├── 📄 ApplicationController.cs ← Application management
│   │   │   └── 📄 AccessController.cs  ← Access control
│   │   │
│   │   ├── 📁 Parts/                   ← Content parts
│   │   │   ├── 📄 ApplicationPart.cs   ← Application content part
│   │   │   └── 📄 AccessPart.cs        ← Access content part
│   │   │
│   │   ├── 📁 Drivers/                 ← ContentPart drivers
│   │   │   ├── 📄 ApplicationPartDriver.cs ← Application part driver
│   │   │   └── 📄 AccessPartDriver.cs  ← Access part driver
│   │   │
│   │   ├── 📁 ViewModels/              ← View models
│   │   │   ├── 📄 ApplicationViewModel.cs ← Application forms
│   │   │   └── 📄 AccessViewModel.cs   ← Access forms
│   │   │
│   │   ├── 📁 Views/                   ← Application views
│   │   │   ├── 📁 Application/         ← Application views
│   │   │   │   ├── 📄 Index.cshtml     ← Application list
│   │   │   │   └── 📄 Details.cshtml   ← Application details
│   │   │   └── 📁 Parts/
│   │   │       ├── 📄 Application.cshtml ← Application display
│   │   │       └── 📄 Access.cshtml    ← Access display
│   │   │
│   │   ├── 📁 GraphQL/                 ← GraphQL support
│   │   │   └── 📄 ApplicationQueries.cs ← Application GraphQL queries
│   │   │
│   │   ├── 📁 Permissions/             ← Module permissions
│   │   │   └── 📄 ApplicationPermissions.cs
│   │   │
│   │   └── 📁 Recipes/                 ← Module setup
│   │       └── 📄 application-setup.recipe.json ← Application module setup
│   │
│   ├── 📁 HoangNgoc.Comment/           ← Comment module
│   │   ├── 📄 HoangNgoc.Comment.csproj ← Module project file
│   │   ├── 📄 Manifest.cs              ← Module manifest
│   │   ├── 📄 Startup.cs               ← Module startup
│   │   │
│   │   ├── 📁 Services/                ← Comment services
│   │   │   └── 📄 CommentService.cs    ← Comment management
│   │   │
│   │   ├── 📁 Controllers/             ← Comment controllers
│   │   │   └── 📄 CommentController.cs ← Comment management
│   │   │
│   │   ├── 📁 Parts/                   ← Content parts
│   │   │   └── 📄 CommentPart.cs       ← Comment content part
│   │   │
│   │   ├── 📁 Drivers/                 ← ContentPart drivers
│   │   │   └── 📄 CommentPartDriver.cs ← Comment part driver
│   │   │
│   │   ├── 📁 ViewModels/              ← View models
│   │   │   └── 📄 CommentViewModel.cs  ← Comment forms
│   │   │
│   │   ├── 📁 Views/                   ← Comment views
│   │   │   └── 📁 Parts/
│   │   │       └── 📄 Comment.cshtml   ← Comment display
│   │   │
│   │   └── 📁 Permissions/             ← Module permissions
│   │       └── 📄 CommentPermissions.cs
│   │
│   └── 📁 HoangNgoc.Extensions/        ← Extensions module
│       ├── 📄 HoangNgoc.Extensions.csproj ← Module project file
│       ├── 📄 Manifest.cs              ← Module manifest
│       ├── 📄 Startup.cs               ← Module startup
│       │
│       ├── 📁 Services/                ← Extension services
│       │   ├── 📄 EmailService.cs      ← Email service
│       │   ├── 📄 SmsService.cs        ← SMS service
│       │   └── 📄 NotificationService.cs ← Notification service
│       │
│       ├── 📁 Controllers/             ← Extension controllers
│       │   └── 📄 NotificationController.cs ← Notification management
│       │
│       ├── 📁 ViewModels/              ← View models
│       │   └── 📄 NotificationViewModel.cs ← Notification forms
│       │
│       ├── 📁 Views/                   ← Extension views
│       │   └── 📁 Notification/
│       │       └── 📄 Index.cshtml     ← Notification list
│       │
│       └── 📁 Permissions/             ← Module permissions
│           └── 📄 ExtensionPermissions.cs
│
└── 📁 Themes/                          ← Themes folder
    └── 📁 HoangNgocCustomizations/     ← TheAdmin customizations
        ├── 📄 Theme.png                ← Theme icon
        ├── 📄 Manifest.cs              ← Theme manifest
        ├── 📄 Placement.json           ← Shape placement
        │
        ├── 📁 Views/                   ← Theme views
        │   ├── 📄 Layout.cshtml        ← Main layout
        │   ├── 📄 _ViewImports.cshtml  ← View imports
        │   ├── 📄 _ViewStart.cshtml    ← View start
        │   │
        │   ├── 📁 Home/                ← Home views
        │   │   └── 📄 Index.cshtml     ← Homepage
        │   │
        │   ├── 📁 Shared/              ← Shared views
        │   │   ├── 📄 _Layout.cshtml   ← Shared layout
        │   │   └── 📄 Error.cshtml     ← Error page
        │   │
        │   └── 📁 Items/               ← Content item views
        │       ├── 📄 Content.cshtml   ← Default content view
        │       └── 📄 Page.cshtml      ← Page content view
        │
        ├── 📁 wwwroot/                 ← Theme assets
        │   ├── 📁 css/                 ← Custom CSS
        │   │   ├── 📄 site.css         ← Main stylesheet
        │   │   └── 📄 admin.css        ← Admin customizations
        │   ├── 📁 js/                  ← Custom JavaScript
        │   │   └── 📄 site.js          ← Main script
        │   └── 📁 images/              ← Theme images
        │       └── 📄 logo.png         ← Site logo
        │
        └── 📁 Recipes/                 ← Theme setup
            └── 📄 theme-setup.recipe.json ← Theme configuration
```

## 📋 TỔNG QUAN CÁC PHASE TRIỂN KHAI (CHO AI - CHỈ VIẾT CODE)

### **PHASE 1: PROJECT FOUNDATION & USER AUTHENTICATION (3 sessions - 0.75 tuần)**

**Session 1.1: Solution & Project Setup (3 giờ)**

**Bước 1.1.1: Tạo solution và project files (45 phút)**
1.1.1.1: Tạo HoangNgocWebsite.sln file (15 phút)
1.1.1.2: Tạo HoangNgocWebsite.csproj với OrchardCore 2.2.1 packages (15 phút)
1.1.1.3: Tạo global.json và Directory.Build.props (15 phút)

**Bước 1.1.2: Tạo Program.cs và startup configuration (45 phút)**
1.1.2.1: Viết Program.cs với OrchardCore services (30 phút)
1.1.2.2: Cấu hình middleware pipeline (15 phút)

**Bước 1.1.3: Tạo configuration files (45 phút)**
1.1.3.1: Tạo appsettings.json với database connection (15 phút)
1.1.3.2: Tạo appsettings.Development.json (15 phút)
1.1.3.3: Tạo NLog.config cho logging (15 phút)

**Bước 1.1.4: Tạo basic structure files (45 phút)**
1.1.4.1: Tạo .gitignore file (15 phút)
1.1.4.2: Tạo README.md documentation (15 phút)
1.1.4.3: Tạo web.config cho IIS (15 phút)

**Session 1.2: User Authentication & Registration Setup (3 giờ)**

**Bước 1.2.1: Enable OrchardCore Users module (45 phút)**
1.2.1.1: Enable OrchardCore.Users trong Program.cs (15 phút)
1.2.1.2: Enable OrchardCore.Users.Registration (15 phút)
1.2.1.3: Cấu hình user settings trong appsettings.json (15 phút)

**Bước 1.2.2: Tạo custom user registration views (45 phút)**
1.2.2.1: Tạo Views/Account/Register.cshtml (15 phút)
1.2.2.2: Tạo Views/Account/Login.cshtml (15 phút)
1.2.2.3: Tạo Views/Account/Profile.cshtml (15 phút)

**Bước 1.2.3: Cấu hình user roles và permissions (45 phút)**
1.2.3.1: Tạo custom roles (Member, Premium, Admin) (15 phút)
1.2.3.2: Cấu hình role permissions (15 phút)
1.2.3.3: Tạo user registration workflow (15 phút)

**Bước 1.2.4: Tạo user profile extensions (45 phút)**
1.2.4.1: Tạo UserProfilePart.cs (15 phút)
1.2.4.2: Tạo UserProfilePartDriver.cs (15 phút)
1.2.4.3: Tạo user profile views (15 phút)

**Session 1.3: Basic Theme & User Interface (3 giờ)**

**Bước 1.3.1: Tạo HoangNgocCustomizations theme structure (45 phút)**
1.3.1.1: Tạo theme folder structure (15 phút)
1.3.1.2: Tạo Manifest.cs cho theme (15 phút)
1.3.1.3: Tạo basic Placement.json (15 phút)

**Bước 1.3.2: Tạo basic layout files (45 phút)**
1.3.2.1: Tạo Layout.cshtml với user menu (15 phút)
1.3.2.2: Tạo _ViewImports.cshtml và _ViewStart.cshtml (15 phút)
1.3.2.3: Tạo Home/Index.cshtml với login/register links (15 phút)

**Bước 1.3.3: Tạo user navigation components (45 phút)**
1.3.3.1: Tạo user login/logout menu (15 phút)
1.3.3.2: Tạo user profile dropdown (15 phút)
1.3.3.3: Tạo user dashboard navigation (15 phút)

**Bước 1.3.4: Tạo theme assets (45 phút)**
1.3.4.1: Tạo site.css với user interface styling (15 phút)
1.3.4.2: Tạo site.js với user interaction (15 phút)
1.3.4.3: Tạo theme recipe file (15 phút)

**Bước 1.2.2: Tạo basic layout files (45 phút)**
1.2.2.1: Tạo Layout.cshtml (15 phút)
1.2.2.2: Tạo _ViewImports.cshtml và _ViewStart.cshtml (15 phút)
1.2.2.3: Tạo basic Home/Index.cshtml (15 phút)

**Bước 1.2.3: Tạo theme assets (45 phút)**
1.2.3.1: Tạo site.css với basic styling (15 phút)
1.2.3.2: Tạo site.js với basic functionality (15 phút)
1.2.3.3: Tạo theme recipe file (15 phút)

**Bước 1.2.4: Tạo docker configuration (45 phút)**
1.2.4.1: Tạo Dockerfile (15 phút)
1.2.4.2: Tạo docker-compose.yml (15 phút)
1.2.4.3: Tạo .dockerignore (15 phút)

### **PHASE 2: USER MANAGEMENT & WALLET MODULE (3 sessions - 0.75 tuần)**

**Session 2.1: User Registration & Authentication (3 giờ)**

**Bước 2.1.1: Tạo HoangNgoc.Users project (45 phút)**
2.1.1.1: Tạo HoangNgoc.Users.csproj (15 phút)
2.1.1.2: Tạo Manifest.cs (15 phút)
2.1.1.3: Tạo Startup.cs với user services (15 phút)

**Bước 2.1.2: Tạo user models và parts (45 phút)**
2.1.2.1: Tạo UserProfilePart.cs (15 phút)
2.1.2.2: Tạo WalletPart.cs với Balance, IsActive properties (15 phút)
2.1.2.3: Tạo TransactionModel.cs với Amount, Type, Status, CreatedDate (15 phút)

**Bước 2.1.3: Tạo user services (45 phút)**
2.1.3.1: Tạo UserRegistrationService.cs (15 phút)
2.1.3.2: Tạo UserProfileService.cs (15 phút)
2.1.3.3: Tạo WalletService.cs với GetBalance, AddFunds, DeductFunds (15 phút)

**Bước 2.1.4: Tạo account controllers (45 phút)**
2.1.4.1: Tạo AccountController.cs với Register, Login, Profile actions (30 phút)
2.1.4.2: Tạo user view models (RegisterViewModel, LoginViewModel, ProfileViewModel) (15 phút)

**Session 2.2: Wallet Management & Top-up System (3 giờ)**

**Bước 2.2.1: Tạo wallet controllers (45 phút)**
2.2.1.1: Tạo WalletController.cs với Dashboard, TopUp, History actions (30 phút)
2.2.1.2: Tạo UserDashboardController.cs (15 phút)

**Bước 2.2.2: Tạo top-up request system (45 phút)**
2.2.2.1: Tạo TopUpRequestModel.cs với UserId, Amount, BankTransferInfo, Status (15 phút)
2.2.2.2: Tạo TopUpViewModel.cs cho user request form (15 phút)
2.2.2.3: Tạo TransactionService.cs để quản lý transactions (15 phút)

**Bước 2.2.3: Tạo admin wallet management (45 phút)**
2.2.2.1: Tạo AdminWalletController.cs cho admin approve top-up (30 phút)
2.2.2.2: Tạo admin views để duyệt nạp tiền (15 phút)

**Bước 2.2.4: Tạo wallet views (45 phút)**
2.2.4.1: Tạo Wallet/Dashboard.cshtml - hiển thị số dư và lịch sử (15 phút)
2.2.4.2: Tạo Wallet/TopUp.cshtml - form yêu cầu nạp tiền (15 phút)
2.2.4.3: Tạo Wallet/History.cshtml - lịch sử giao dịch (15 phút)

**Session 2.3: User Interface & Integration (3 giờ)**

**Bước 2.3.1: Tạo user account views (45 phút)**
2.3.1.1: Tạo Account/Register.cshtml (15 phút)
2.3.1.2: Tạo Account/Login.cshtml (15 phút)
2.3.1.3: Tạo Account/Profile.cshtml và Dashboard.cshtml (15 phút)

**Bước 2.3.2: Tạo content part drivers (45 phút)**
2.3.2.1: Tạo UserProfilePartDriver.cs (15 phút)
2.3.2.2: Tạo WalletPartDriver.cs (15 phút)
2.3.2.3: Tạo user part views (UserProfile.cshtml, WalletWidget.cshtml) (15 phút)

**Bước 2.3.3: Tạo user workflows (45 phút)**
2.3.3.1: Tạo UserRegistrationActivity.cs (15 phút)
2.3.3.2: Tạo WalletTopUpActivity.cs - workflow cho admin approve (15 phút)
2.3.3.3: Tạo TransactionActivity.cs (15 phút)

**Bước 2.3.4: Tạo database indexes và migrations (45 phút)**
2.3.4.1: Tạo UserProfileIndex.cs, WalletIndex.cs, TransactionIndex.cs (15 phút)
2.3.4.2: Tạo Migrations.cs cho user tables (15 phút)
2.3.4.3: Tạo user-setup.recipe.json và user-roles.recipe.json (15 phút)

### **PHASE 3: CORE MODULE (2 sessions - 0.5 tuần)**

**Session 2.1: Core Module Foundation (3 giờ)**

**Bước 2.1.1: Tạo HoangNgoc.Core project (45 phút)**
2.1.1.1: Tạo HoangNgoc.Core.csproj (15 phút)
2.1.1.2: Tạo Manifest.cs (15 phút)
2.1.1.3: Tạo Startup.cs (15 phút)

**Bước 2.1.2: Tạo shared interfaces (45 phút)**
2.1.2.1: Tạo IPaymentService.cs interface (15 phút)
2.1.2.2: Tạo ICourseService.cs interface (15 phút)
2.1.2.3: Tạo IApplicationService.cs interface (15 phút)

**Bước 2.1.3: Tạo shared models (45 phút)**
2.1.3.1: Tạo PaymentResult.cs model (15 phút)
2.1.3.2: Tạo CourseEnrollment.cs model (15 phút)
2.1.3.3: Tạo ApplicationUsage.cs model (15 phút)

**Bước 2.1.4: Tạo permissions và extensions (45 phút)**
2.1.4.1: Tạo CorePermissions.cs (15 phút)
2.1.4.2: Tạo ContentExtensions.cs (15 phút)
2.1.4.3: Tạo utility helper classes (15 phút)

**Session 2.2: Core Recipes & Configuration (3 giờ)**

**Bước 2.2.1: Tạo setup recipes (45 phút)**
2.2.1.1: Tạo HoangNgocSite.recipe.json (30 phút)
2.2.1.2: Tạo HoangNgocContent.recipe.json (15 phút)

**Bước 2.2.2: Tạo migration scripts (45 phút)**
2.2.2.1: Tạo core migrations (30 phút)
2.2.2.2: Tạo content type definitions (15 phút)

**Bước 2.2.3: Tạo configuration helpers (45 phút)**
2.2.3.1: Tạo ConfigurationExtensions.cs (15 phút)
2.2.3.2: Tạo LoggingExtensions.cs (15 phút)
2.2.3.3: Tạo ValidationHelpers.cs (15 phút)

**Bước 2.2.4: Tạo unit tests (45 phút)**
2.2.4.1: Tạo test project structure (15 phút)
2.2.4.2: Viết tests cho interfaces (15 phút)
2.2.4.3: Viết tests cho extensions (15 phút)

### **PHASE 3: PAYMENT MODULE (4 sessions - 1 tuần)**

**Session 3.1: Payment Module Foundation (3 giờ)**

**Bước 3.1.1: Tạo HoangNgoc.Payment project (45 phút)**
3.1.1.1: Tạo HoangNgoc.Payment.csproj (15 phút)
3.1.1.2: Tạo Manifest.cs (15 phút)
3.1.1.3: Tạo Startup.cs với DI configuration (15 phút)

**Bước 3.1.2: Tạo payment models và parts (45 phút)**
3.1.2.1: Tạo PaymentPart.cs (15 phút)
3.1.2.2: Tạo WalletPart.cs (15 phút)
3.1.2.3: Tạo TransactionModel.cs (15 phút)

**Bước 3.1.3: Tạo database indexes (45 phút)**
3.1.3.1: Tạo PaymentIndex.cs (15 phút)
3.1.3.2: Tạo WalletIndex.cs (15 phút)
3.1.3.3: Tạo Migrations.cs (15 phút)

**Bước 3.1.4: Tạo permissions và recipes (45 phút)**
3.1.4.1: Tạo PaymentPermissions.cs (15 phút)
3.1.4.2: Tạo payment-setup.recipe.json (15 phút)
3.1.4.3: Tạo payment-contenttypes.recipe.json (15 phút)

**Session 3.2: Payment Services & Gateways (3 giờ)**

**Bước 3.2.1: Tạo payment gateway services (45 phút)**
3.2.1.1: Tạo VNPayService.cs (15 phút)
3.2.1.2: Tạo MoMoService.cs (15 phút)
3.2.1.3: Tạo ZaloPayService.cs (15 phút)

**Bước 3.2.2: Tạo wallet service với đầy đủ tính năng (45 phút)**
3.2.2.1: Tạo WalletService.cs với các method: GetBalance, AddFunds, DeductFunds, GetTransactionHistory (30 phút)
3.2.2.2: Tạo payment validation logic và security checks (15 phút)

**Bước 3.2.3: Tạo payment controllers (45 phút)**
3.2.3.1: Tạo PaymentController.cs với deposit request handling (15 phút)
3.2.3.2: Tạo CallbackController.cs cho gateway callbacks (15 phút)
3.2.3.3: Tạo AdminWalletController.cs cho admin nạp tiền thủ công (15 phút)

**Bước 3.2.4: Tạo view models (45 phút)**
3.2.4.1: Tạo PaymentViewModel.cs (15 phút)
3.2.4.2: Tạo WalletViewModel.cs (15 phút)
3.2.4.3: Tạo validation attributes (15 phút)

**Session 3.3: Payment UI & Drivers (3 giờ)**

**Bước 3.3.1: Tạo content part drivers (45 phút)**
3.3.1.1: Tạo PaymentPartDriver.cs (30 phút)
3.3.1.2: Tạo WalletPartDriver.cs (15 phút)

**Bước 3.3.2: Tạo content handlers (45 phút)**
3.3.2.1: Tạo PaymentContentHandler.cs (30 phút)
3.3.2.2: Tạo payment event handlers (15 phút)

**Bước 3.3.3: Tạo admin views (45 phút)**
3.3.3.1: Tạo Payment/Index.cshtml (15 phút)
3.3.3.2: Tạo Payment/Create.cshtml (15 phút)
3.3.3.3: Tạo Payment/Edit.cshtml (15 phút)

**Bước 3.3.4: Tạo frontend views (45 phút)**
3.3.4.1: Tạo PaymentFrontend/Index.cshtml (15 phút)
3.3.4.2: Tạo PaymentFrontend/Success.cshtml (15 phút)
3.3.4.3: Tạo Parts/Wallet.cshtml (15 phút)

**Session 3.4: Wallet Management & User Dashboard (3 giờ)**

**Bước 3.4.1: Tạo wallet dashboard views (45 phút)**
3.4.1.1: Tạo Wallet/Dashboard.cshtml - hiển thị số dư, lịch sử giao dịch (15 phút)
3.4.1.2: Tạo Wallet/TopUp.cshtml - form nạp tiền (15 phút)
3.4.1.3: Tạo Wallet/History.cshtml - lịch sử giao dịch chi tiết (15 phút)

**Bước 3.4.2: Tạo wallet controllers (45 phút)**
3.4.2.1: Tạo WalletController.cs với actions: Dashboard, TopUp, History, GetBalance (30 phút)
3.4.2.2: Tạo wallet API endpoints cho AJAX calls (15 phút)

**Bước 3.4.3: Tạo wallet workflow activities (45 phút)**
3.4.3.1: Tạo TopUpWorkflowActivity.cs - xử lý nạp tiền (15 phút)
3.4.3.2: Tạo DeductFundsWorkflowActivity.cs - xử lý trừ tiền (15 phút)
3.4.3.3: Tạo TransactionNotificationActivity.cs - thông báo giao dịch (15 phút)

**Bước 3.4.4: Tạo wallet GraphQL & security (45 phút)**
3.4.4.1: Tạo WalletQueries.cs - query balance, transactions (15 phút)
3.4.4.2: Tạo WalletMutations.cs - topup, transfer mutations (15 phút)
3.4.4.3: Implement wallet security & validation (15 phút)

**Session 3.5: Payment Integration & Testing (3 giờ)**

**Bước 3.5.1: Tạo payment integration (45 phút)**
3.5.1.1: Integrate wallet với payment gateways (15 phút)
3.5.1.2: Tạo payment success/failure handlers (15 phút)
3.5.1.3: Implement automatic wallet top-up (15 phút)

**Bước 3.5.2: Tạo Liquid filters và tags (45 phút)**
3.5.2.1: Tạo PaymentFilters.cs (15 phút)
3.5.2.2: Tạo WalletFilters.cs với balance, transaction filters (15 phút)
3.5.2.3: Tạo custom Liquid tags: {% wallet_balance %}, {% transaction_history %} (15 phút)

**Bước 3.5.3: Tạo user wallet integration (45 phút)**
3.5.3.1: Integrate wallet với user profile (15 phút)
3.5.3.2: Tạo wallet widget cho user dashboard (15 phút)
3.5.3.3: Implement wallet permissions per user (15 phút)

**Bước 3.5.4: Tạo tests và validation (45 phút)**
3.5.4.1: Tạo wallet unit tests (15 phút)
3.5.4.2: Tạo payment integration tests (15 phút)
3.5.4.3: Tạo Placement.json và final validation (15 phút)

### **PHASE 4: TRAINING MODULE (4 sessions - 1 tuần)**

**Session 4.1: Training Module Foundation (3 giờ)**

**Bước 4.1.1: Tạo HoangNgoc.Training project (45 phút)**
4.1.1.1: Tạo HoangNgoc.Training.csproj (15 phút)
4.1.1.2: Tạo Manifest.cs (15 phút)
4.1.1.3: Tạo Startup.cs (15 phút)

**Bước 4.1.2: Tạo training models và parts (45 phút)**
4.1.2.1: Tạo CoursePart.cs (15 phút)
4.1.2.2: Tạo LessonPart.cs (15 phút)
4.1.2.3: Tạo ProgressPart.cs (15 phút)

**Bước 4.1.3: Tạo database indexes (45 phút)**
4.1.3.1: Tạo CourseIndex.cs (15 phút)
4.1.3.2: Tạo LessonIndex.cs (15 phút)
4.1.3.3: Tạo Migrations.cs (15 phút)

**Bước 4.1.4: Tạo permissions và recipes (45 phút)**
4.1.4.1: Tạo TrainingPermissions.cs (15 phút)
4.1.4.2: Tạo training-setup.recipe.json (15 phút)
4.1.4.3: Tạo training-contenttypes.recipe.json (15 phút)

**Session 4.2: Training Services (3 giờ)**

**Bước 4.2.1: Tạo course service (45 phút)**
4.2.1.1: Tạo CourseService.cs (30 phút)
4.2.1.2: Tạo course business logic (15 phút)

**Bước 4.2.2: Tạo lesson service (45 phút)**
4.2.2.1: Tạo LessonService.cs (30 phút)
4.2.2.2: Tạo lesson delivery logic (15 phút)

**Bước 4.2.3: Tạo progress service (45 phút)**
4.2.3.1: Tạo ProgressService.cs (30 phút)
4.2.3.2: Tạo progress tracking logic (15 phút)

**Bước 4.2.4: Tạo certificate service (45 phút)**
4.2.4.1: Tạo CertificateService.cs (30 phút)
4.2.4.2: Tạo certificate generation logic (15 phút)

**Session 4.3: Training Controllers & UI (3 giờ)**

**Bước 4.3.1: Tạo training controllers (45 phút)**
4.3.1.1: Tạo CourseController.cs (15 phút)
4.3.1.2: Tạo LessonController.cs (15 phút)
4.3.1.3: Tạo CertificateController.cs (15 phút)

**Bước 4.3.2: Tạo view models (45 phút)**
4.3.2.1: Tạo CourseViewModel.cs (15 phút)
4.3.2.2: Tạo LessonViewModel.cs (15 phút)
4.3.2.3: Tạo ProgressViewModel.cs (15 phút)

**Bước 4.3.3: Tạo content part drivers (45 phút)**
4.3.3.1: Tạo CoursePartDriver.cs (15 phút)
4.3.3.2: Tạo LessonPartDriver.cs (15 phút)
4.3.3.3: Tạo ProgressPartDriver.cs (15 phút)

**Bước 4.3.4: Tạo course views (45 phút)**
4.3.4.1: Tạo Course/Index.cshtml (15 phút)
4.3.4.2: Tạo Course/Details.cshtml (15 phút)
4.3.4.3: Tạo Course/Enroll.cshtml (15 phút)

**Session 4.4: Training Workflows & Extensions (3 giờ)**

**Bước 4.4.1: Tạo lesson views và parts (45 phút)**
4.4.1.1: Tạo Lesson/Index.cshtml (15 phút)
4.4.1.2: Tạo Lesson/Player.cshtml (15 phút)
4.4.1.3: Tạo Parts/Course.cshtml, Lesson.cshtml, Progress.cshtml (15 phút)

**Bước 4.4.2: Tạo workflow activities (45 phút)**
4.4.2.1: Tạo EnrollmentActivity.cs (15 phút)
4.4.2.2: Tạo ProgressActivity.cs (15 phút)
4.4.2.3: Tạo completion workflows (15 phút)

**Bước 4.4.3: Tạo GraphQL support (45 phút)**
4.4.3.1: Tạo CourseQueries.cs (15 phút)
4.4.3.2: Tạo ProgressMutations.cs (15 phút)
4.4.3.3: Cấu hình GraphQL schema (15 phút)

**Bước 4.4.4: Tạo Liquid filters và tests (45 phút)**
4.4.4.1: Tạo CourseFilters.cs (15 phút)
4.4.4.2: Tạo ProgressFilters.cs (15 phút)
4.4.4.3: Tạo training unit tests (15 phút)

### **PHASE 5: APPLICATION MODULE (2 sessions - 0.5 tuần)**

**Session 5.1: Application Module Foundation (3 giờ)**

**Bước 5.1.1: Tạo HoangNgoc.Application project (45 phút)**
5.1.1.1: Tạo HoangNgoc.Application.csproj (15 phút)
5.1.1.2: Tạo Manifest.cs (15 phút)
5.1.1.3: Tạo Startup.cs (15 phút)

**Bước 5.1.2: Tạo application models và parts (45 phút)**
5.1.2.1: Tạo ApplicationPart.cs (15 phút)
5.1.2.2: Tạo AccessPart.cs (15 phút)
5.1.2.3: Tạo Migrations.cs (15 phút)

**Bước 5.1.3: Tạo application services (45 phút)**
5.1.3.1: Tạo ApplicationService.cs (15 phút)
5.1.3.2: Tạo AccessService.cs (15 phút)
5.1.3.3: Tạo UsageService.cs (15 phút)

**Bước 5.1.4: Tạo controllers và view models (45 phút)**
5.1.4.1: Tạo ApplicationController.cs (15 phút)
5.1.4.2: Tạo AccessController.cs (15 phút)
5.1.4.3: Tạo ApplicationViewModel.cs và AccessViewModel.cs (15 phút)

**Session 5.2: Application UI & Integration (3 giờ)**

**Bước 5.2.1: Tạo content part drivers (45 phút)**
5.2.1.1: Tạo ApplicationPartDriver.cs (30 phút)
5.2.1.2: Tạo AccessPartDriver.cs (15 phút)

**Bước 5.2.2: Tạo application views (45 phút)**
5.2.2.1: Tạo Application/Index.cshtml (15 phút)
5.2.2.2: Tạo Application/Details.cshtml (15 phút)
5.2.2.3: Tạo Parts/Application.cshtml và Access.cshtml (15 phút)

**Bước 5.2.3: Tạo GraphQL và permissions (45 phút)**
5.2.3.1: Tạo ApplicationQueries.cs (15 phút)
5.2.3.2: Tạo ApplicationPermissions.cs (15 phút)
5.2.3.3: Tạo application-setup.recipe.json (15 phút)

**Bước 5.2.4: Tạo tests và integration (45 phút)**
5.2.4.1: Tạo application unit tests (15 phút)
5.2.4.2: Integration với payment module (15 phút)
5.2.4.3: Tạo content handlers (15 phút)

### **PHASE 6: COMMENT & EXTENSIONS MODULES (1 session - 0.25 tuần)**

**Session 6.1: Comment & Extensions Implementation (3 giờ)**

**Bước 6.1.1: Tạo HoangNgoc.Comment module (45 phút)**
6.1.1.1: Tạo project structure (15 phút)
6.1.1.2: Tạo CommentPart.cs và CommentPartDriver.cs (15 phút)
6.1.1.3: Tạo CommentController.cs và views (15 phút)

**Bước 6.1.2: Tạo HoangNgoc.Extensions module (45 phút)**
6.1.2.1: Tạo project structure (15 phút)
6.1.2.2: Tạo EmailService.cs (15 phút)
6.1.2.3: Tạo SmsService.cs (15 phút)

**Bước 6.1.3: Tạo notification services (45 phút)**
6.1.3.1: Tạo NotificationService.cs (15 phút)
6.1.3.2: Tạo NotificationController.cs (15 phút)
6.1.3.3: Tạo notification views (15 phút)

**Bước 6.1.4: Hoàn thiện extensions (45 phút)**
6.1.4.1: Tạo ExtensionPermissions.cs (15 phút)
6.1.4.2: Tạo NotificationViewModel.cs (15 phút)
6.1.4.3: Tạo unit tests cho extensions (15 phút)

### **PHASE 7: THEME CUSTOMIZATION & FINALIZATION (3 sessions - 0.75 tuần)**

**Session 7.1: Advanced Theme Customization (3 giờ)**

**Bước 7.1.1: Tạo advanced layout templates (45 phút)**
7.1.1.1: Tạo custom shape templates (15 phút)
7.1.1.2: Cập nhật Placement.json với advanced placement (15 phút)
7.1.1.3: Tạo theme-specific views (15 phút)

**Bước 7.1.2: Tạo responsive CSS (45 phút)**
7.1.2.1: Cập nhật site.css với responsive design (30 phút)
7.1.2.2: Tạo admin.css cho admin customizations (15 phút)

**Bước 7.1.3: Tạo JavaScript functionality (45 phút)**
7.1.3.1: Cập nhật site.js với interactive features (30 phút)
7.1.3.2: Tạo module-specific JavaScript (15 phút)

**Bước 7.1.4: Tạo content item views (45 phút)**
7.1.4.1: Tạo Items/Content.cshtml (15 phút)
7.1.4.2: Tạo Items/Page.cshtml (15 phút)
7.1.4.3: Tạo Shared/Error.cshtml (15 phút)

**Session 7.2: Integration Testing & Optimization (3 giờ)**

**Bước 7.2.1: Tạo comprehensive tests (45 phút)**
7.2.1.1: Tạo integration tests cho tất cả modules (30 phút)
7.2.1.2: Tạo end-to-end tests (15 phút)

**Bước 7.2.2: Performance optimization (45 phút)**
7.2.2.1: Optimize database queries (15 phút)
7.2.2.2: Optimize CSS và JavaScript (15 phút)
7.2.2.3: Cấu hình caching strategies (15 phút)

**Bước 7.2.3: Security hardening (45 phút)**
7.2.3.1: Implement security headers (15 phút)
7.2.3.2: Validate input sanitization (15 phút)
7.2.3.3: Implement CSRF protection (15 phút)

**Bước 7.2.4: Code cleanup và documentation (45 phút)**
7.2.4.1: Code review và cleanup (15 phút)
7.2.4.2: Cập nhật XML documentation (15 phút)
7.2.4.3: Cập nhật README.md (15 phút)

**Session 7.3: Final Testing & Package Preparation (3 giờ)**

**Bước 7.3.1: Final integration testing (45 phút)**
7.3.1.1: Test tất cả workflows (15 phút)
7.3.1.2: Test payment integration (15 phút)
7.3.1.3: Test training workflows (15 phút)

**Bước 7.3.2: Package preparation (45 phút)**
7.3.2.1: Tạo deployment scripts (15 phút)
7.3.2.2: Tạo database migration scripts (15 phút)
7.3.2.3: Tạo configuration templates (15 phút)

**Bước 7.3.3: Documentation completion (45 phút)**
7.3.3.1: Hoàn thiện technical documentation (15 phút)
7.3.3.2: Tạo API documentation (15 phút)
7.3.3.3: Tạo deployment guide (15 phút)

**Bước 7.3.4: Final validation (45 phút)**
7.3.4.1: Final code review (15 phút)
7.3.4.2: Final testing (15 phút)
7.3.4.3: Package delivery preparation (15 phút)

## 📋 TIMELINE TỔNG QUAN (CHO AI)

**TỔNG THỜI GIAN: 5 tuần (20 sessions)**

- **PHASE 1**: Project Foundation & User Authentication - 0.75 tuần (3 sessions)
- **PHASE 2**: User Management & Wallet Module - 0.75 tuần (3 sessions)
- **PHASE 3**: Core Module - 0.5 tuần (2 sessions)  
- **PHASE 4**: Payment Module - 1.25 tuần (5 sessions)
- **PHASE 5**: Training Module - 1 tuần (4 sessions)
- **PHASE 6**: Application Module - 0.5 tuần (2 sessions)
- **PHASE 7**: Comment & Extensions - 0.25 tuần (1 session)
- **PHASE 8**: Theme & Finalization - 0.75 tuần (3 sessions)

**TỔNG CỘNG: 60 giờ làm việc (chỉ viết code)**

## 🎯 CÁC TÍNH NĂNG CHÍNH ĐÃ BAO GỒM:

✅ **Đăng ký/Đăng nhập**: Module HoangNgoc.Users với AccountController
✅ **Tài khoản cá nhân**: UserProfilePart với dashboard
✅ **Ví tiền cá nhân**: WalletPart với số dư cho từng user
✅ **Nạp tiền**: TopUpRequestModel - user gửi yêu cầu, admin duyệt thủ công
✅ **Quản lý số dư**: WalletService với GetBalance, AddFunds, DeductFunds
✅ **Lịch sử giao dịch**: TransactionModel với đầy đủ thông tin giao dịch
✅ **Admin quản lý**: AdminWalletController để admin duyệt nạp tiền

## 🎨 MÔ TẢ GIAO DIỆN CHI TIẾT

### **THIẾT KẾ TỔNG THỂ**
- **Màu sắc chính**: Dark blue/Navy (#1e3a5f), Light blue (#4a90e2), White, Gray
- **Layout**: Responsive, modern, professional
- **Typography**: Clean, readable fonts (similar to reference image)

### **1. HEADER NAVIGATION**
- **Logo**: "HOÀNG NGỌC" (trái)
- **Main Menu** (giữa):
  - TRANG CHỦ
  - GIỚI THIỆU (dropdown)
    - Giới thiệu về đơn vị
    - Sứ mệnh – tầm nhìn – giá trị cốt lõi  
    - Cơ cấu tổ chức
  - TIN TỨC (dropdown)
    - Văn bản pháp luật
    - Công nghệ và Kỹ thuật xây dựng
    - Chuyển đổi số trong xây dựng
    - Đô thị thông minh
    - Công trình, dự án tiêu biểu
  - ỨNG DỤNG (dropdown)
    - Phần mềm vẽ kỹ thuật
    - Phần mềm phân tích tính toán
    - Tạo lập và quản lý Hồ sơ công trình
    - Hệ thống quản lý
    - Thiết kế theo đặt hàng (sub-menu: thiết kế nhà, công trình, web)
  - ĐÀO TẠO (dropdown)
    - Các khóa học
    - Tài liệu học tập
  - DIỄN ĐÀN (Discourse integration)
  - LIÊN HỆ
  - HƯỚNG DẪN
- **Right Side**:
  - Language selector (VN/EN)
  - User menu (nếu đã login) hoặc "Đăng nhập" button
  - "Bắt đầu" button (primary CTA)

### **2. TRANG CHỦ (Homepage)**
- **Hero Section** (dark blue background):
  - **Left side**: 
    - Tiêu đề chính: "Hệ thống Quản lý Nhật ký Thi công Chuyên nghiệp"
    - Mô tả: "Giải pháp số hóa toàn diện cho việc quản lý dự án xây dựng..."
    - 2 buttons: "Bắt đầu miễn phí" + "Xem demo"
  - **Right side**: Video thumbnail với play button
- **Features Section**: "Tính năng nổi bật"
  - 3 cards ngang: Quản lý Dự án, Nhật ký Thi công, Quản lý Nhóm
  - Mỗi card có icon, title, description, "Tìm hiểu thêm" link
- **Why Choose Us Section**: "Tại sao chọn chúng tôi?"
- **Footer**: Company info, links, contact

### **3. TRANG ỨNG DỤNG (Applications)**
- **Grid layout** hiển thị các ứng dụng
- **Mỗi app card**:
  - Icon/thumbnail
  - Tên ứng dụng
  - Mô tả ngắn
  - Giá sử dụng (VND/lần sử dụng)
  - Button "Sử dụng ngay" (yêu cầu login + đủ tiền)
- **Filter/Category**: Theo loại ứng dụng
- **Usage tracking**: Hiển thị số lần đã sử dụng

### **4. TRANG ĐĂNG KÝ/ĐĂNG NHẬP**
- **Đăng ký form**:
  - Họ tên, Email, Số điện thoại
  - Username, Password, Confirm Password
  - Checkbox đồng ý điều khoản
  - Button "Đăng ký"
- **Đăng nhập form**:
  - Username/Email, Password
  - "Ghi nhớ đăng nhập" checkbox
  - Button "Đăng nhập"
  - Link "Quên mật khẩu?"

### **5. USER DASHBOARD**
- **Sidebar Menu**:
  - Tổng quan
  - Ví tiền
  - Ứng dụng đã sử dụng
  - Khóa học của tôi
  - Lịch sử giao dịch
  - Cài đặt tài khoản
- **Main Dashboard**:
  - **Wallet Widget**: Số dư hiện tại (prominent)
  - **Quick Actions**: Nạp tiền, Sử dụng ứng dụng
  - **Recent Usage**: Lịch sử sử dụng ứng dụng gần đây
  - **Course Progress**: Tiến độ khóa học đang học

### **6. TRANG VÍ TIỀN**
- **Header**: "Ví tiền của tôi" + Số dư (lớn, nổi bật)
- **Action Cards**:
  - **Nạp tiền**: QR code + thông tin chuyển khoản
  - **Lịch sử**: Xem tất cả giao dịch
- **Payment Methods**:
  - Chuyển khoản ngân hàng (có QR code)
  - VNPay, MoMo, ZaloPay (nếu có)
- **Recent Transactions**: Bảng giao dịch gần đây

### **7. FORM NẠP TIỀN**
- **Bank Transfer Info** (prominent):
  - QR Code để scan
  - Số tài khoản, Tên chủ TK, Ngân hàng
  - Nội dung CK: "NAPVITIEN [USERNAME]"
- **Manual Top-up Form**:
  - Số tiền nạp
  - Ngân hàng chuyển từ
  - Thời gian chuyển khoản
  - Upload ảnh bill
  - Ghi chú
- **Admin Manual Add**: Form riêng cho admin thêm tiền thủ công

### **8. TRANG ĐÀO TẠO**
- **Course Grid**: Hiển thị các khóa học
- **Access Control**: Chỉ hiển thị khóa học được phép truy cập
- **Course Detail**: 
  - Thông tin khóa học
  - Curriculum
  - Progress tracking
  - Video lessons

### **9. DIỄN ĐÀN (DISCOURSE INTEGRATION)**
- **Embedded Discourse forum**
- **Single Sign-On**: Tự động login với tài khoản website
- **Categories**: Theo chủ đề chuyên môn

### **10. ADMIN INTERFACE**
- **User Management**: Quản lý user, phân quyền khóa học
- **Wallet Management**: 
  - Duyệt yêu cầu nạp tiền
  - Thêm tiền thủ công cho user
  - Xem báo cáo tài chính
- **Application Management**: Quản lý ứng dụng, giá cả
- **Content Management**: Quản lý menu, tin tức, khóa học

### **11. RESPONSIVE DESIGN**
- **Mobile**: Hamburger menu, stacked layout
- **Tablet**: Adapted grid layouts
- **Desktop**: Full navigation, multi-column layouts

### **12. UI COMPONENTS**
- **Color Scheme**: Navy blue primary, light blue accent, white/gray backgrounds
- **Buttons**: Rounded corners, hover effects
- **Cards**: Clean shadows, consistent spacing
- **Forms**: Clear labels, validation messages
- **Navigation**: Dropdown menus, breadcrumbs
- **Modals**: For confirmations, quick actions



## 🔨 BUILD & TEST CHECKPOINTS (CHO AI)

**BUILD CHECKPOINT 1.1**: Project setup và basic configuration (15 phút)
**BUILD CHECKPOINT 1.2**: Theme setup và basic layout (15 phút)
**BUILD CHECKPOINT 2.1**: Core module integration (15 phút)
**BUILD CHECKPOINT 2.2**: Core recipes và configuration (15 phút)
**BUILD CHECKPOINT 3.1**: Payment module foundation (15 phút)
**BUILD CHECKPOINT 3.2**: Payment services và gateways (15 phút)
**BUILD CHECKPOINT 3.3**: Payment UI và drivers (15 phút)
**BUILD CHECKPOINT 3.4**: Payment workflows và GraphQL (15 phút)
**BUILD CHECKPOINT 4.1**: Training module foundation (15 phút)
**BUILD CHECKPOINT 4.2**: Training services (15 phút)
**BUILD CHECKPOINT 4.3**: Training controllers và UI (15 phút)
**BUILD CHECKPOINT 4.4**: Training workflows và extensions (15 phút)
**BUILD CHECKPOINT 5.1**: Application module foundation (15 phút)
**BUILD CHECKPOINT 5.2**: Application UI và integration (15 phút)
**BUILD CHECKPOINT 6.1**: Comment và Extensions modules (15 phút)
**BUILD CHECKPOINT 7.1**: Advanced theme customization (15 phút)
**BUILD CHECKPOINT 7.2**: Integration testing và optimization (15 phút)
**BUILD CHECKPOINT 7.3**: Final testing và package preparation (15 phút)

**TỔNG CỘNG: 18 BUILD CHECKPOINTS (4.5 giờ testing)**
