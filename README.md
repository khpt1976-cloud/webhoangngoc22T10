# 🌟 HoangNgoc CMS - OrchardCore Project

**Dự án OrchardCore CMS được tái cấu trúc theo chuẩn OrchardCore pattern với 8 custom modules và 1 custom theme.**

## 🏗️ Cấu Trúc Dự Án

```
HoangNgocProject/
├── src/
│   ├── HoangNgocCMS.Web/                   ← Main Web Application (CLEAN)
│   ├── HoangNgoc.Modules/                  ← 8 Custom Modules (Separate Projects)
│   │   ├── HoangNgoc.Core/                 ← Platform chính
│   │   ├── HoangNgoc.Authentication/       ← Xác thực người dùng
│   │   ├── HoangNgoc.News/                 ← Quản lý tin tức
│   │   ├── HoangNgoc.Comment/              ← Hệ thống bình luận
│   │   ├── HoangNgoc.Training/             ← Đào tạo và khóa học
│   │   ├── HoangNgoc.Payment/              ← Thanh toán
│   │   ├── HoangNgoc.Application/          ← Ứng dụng việc làm
│   │   └── HoangNgoc.Simple/               ← Test module
│   ├── HoangNgoc.Themes/                   ← Custom Themes
│   │   └── HoangNgoc/                      ← HoangNgoc Theme v2.0
│   └── HoangNgoc.Application.Targets/      ← Bundle Package
├── CẤU_TRÚC_CHUẨN_CỦA_DỰ_ÁN_ORCHARDCORE.md ← Tài liệu cấu trúc chuẩn
└── README.md
```

## 🚀 Công Nghệ Sử Dụng

- **Framework:** ASP.NET Core 8.0
- **CMS:** OrchardCore 2.2.1
- **Database:** SQL Server / SQLite
- **Frontend:** Bootstrap, jQuery
- **Architecture:** Modular Architecture Pattern

## 📦 Modules Chính

### 🔧 HoangNgoc.Core
- Platform chính với các services cơ bản
- Wallet system và transaction management
- Application management core

### 🔐 HoangNgoc.Authentication
- Xác thực người dùng tùy chỉnh
- Tích hợp với OrchardCore.Users
- Role-based access control

### 📰 HoangNgoc.News
- Quản lý tin tức và bài viết
- Workflow publishing
- SEO optimization
- Media integration

### 💬 HoangNgoc.Comment
- Hệ thống bình luận
- Moderation tools
- User interaction

### 🎓 HoangNgoc.Training
- Quản lý khóa học
- Lesson tracking
- Progress monitoring
- Certificate system

### 💳 HoangNgoc.Payment
- Tích hợp thanh toán
- Transaction history
- Payment methods

### 💼 HoangNgoc.Application
- Ứng dụng việc làm
- Job posting management
- Candidate tracking
- Recruitment workflow

## 🎨 Theme

### HoangNgoc Theme v2.0
- Responsive design
- Bootstrap-based
- Custom layouts
- SEO optimized

## 🛠️ Cài Đặt và Chạy

### Yêu Cầu Hệ Thống
- .NET 8.0 SDK
- SQL Server hoặc SQLite
- Visual Studio 2022 hoặc VS Code

### Các Bước Cài Đặt

1. **Clone repository:**
   ```bash
   git clone https://github.com/khpt1976-cloud/webhoangngoc20T10.git
   cd webhoangngoc20T10
   ```

2. **Restore packages:**
   ```bash
   cd src/HoangNgocCMS.Web
   dotnet restore
   ```

3. **Build project:**
   ```bash
   dotnet build
   ```

4. **Chạy ứng dụng:**
   ```bash
   dotnet run --urls="http://localhost:5000"
   ```

5. **Truy cập setup:**
   - Mở browser: `http://localhost:5000`
   - Làm theo hướng dẫn setup OrchardCore
   - Tạo admin user và cấu hình database

## 🔧 Cấu Hình

### Database
- **Development:** SQLite (mặc định)
- **Production:** SQL Server

### Admin Account
- **Username:** Hpt
- **Email:** Khpt1976@gmail.com
- **Password:** Hpt@768696

## 📚 Tài Liệu

- [Cấu Trúc Chuẩn OrchardCore](./CẤU_TRÚC_CHUẨN_CỦA_DỰ_ÁN_ORCHARDCORE.md)
- [OrchardCore Documentation](https://docs.orchardcore.net/)

## 🎯 Tính Năng Chính

- ✅ **Modular Architecture** - Cấu trúc module riêng biệt
- ✅ **Custom Authentication** - Xác thực tùy chỉnh
- ✅ **News Management** - Quản lý tin tức
- ✅ **Training System** - Hệ thống đào tạo
- ✅ **Job Application** - Ứng dụng việc làm
- ✅ **Payment Integration** - Tích hợp thanh toán
- ✅ **Comment System** - Hệ thống bình luận
- ✅ **Responsive Theme** - Giao diện responsive

## 🚀 Deployment

### Development
```bash
dotnet run --environment Development
```

### Production
```bash
dotnet publish -c Release
```

## 🤝 Đóng Góp

1. Fork repository
2. Tạo feature branch
3. Commit changes
4. Push to branch
5. Tạo Pull Request

## 📄 License

This project is licensed under the MIT License.

## 📞 Liên Hệ

- **Email:** Khpt1976@gmail.com
- **GitHub:** [khpt1976-cloud](https://github.com/khpt1976-cloud)

---

**🎉 Dự án được tái cấu trúc hoàn toàn theo chuẩn OrchardCore pattern để đảm bảo tính ổn định và khả năng mở rộng!**