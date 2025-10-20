# KẾ HOẠCH TRIỂN KHAI DỰ ÁN HOÀNG NGỌC - ROADMAP CHI TIẾT

## 📊 **TÌNH TRẠNG HIỆN TẠI (Ngày: 18/10/2025)**

### 🎯 **TỔNG QUAN DỰ ÁN:**
- **Repository**: HoangNgocWeb (OrchardCore 2.2.1)
- **Tổng files**: 80 files (.cs, .liquid, .json, .csproj)
- **Tổng dòng code**: 10,284+ dòng
  - Views: 6,379 dòng (.liquid templates)
  - Controllers: 3,905 dòng (.cs files)
- **Modules**: 2 modules chính đã phát triển

### 📈 **TIẾN ĐỘ THEO 8 PHASES:**

| Phase | Tên Phase | Tiến độ | Trạng thái |
|-------|-----------|---------|------------|
| 1 | Project Foundation & User Authentication | 85% | 🔄 Cần hoàn thiện |
| 2 | User Management & Wallet Module | 70% | 🔄 Cần tái cấu trúc |
| 3 | Core Module & Shared Services | 90% | ✅ Gần hoàn thành |
| 4 | Payment Gateway Integration | 60% | 🔄 Cần hoàn thiện |
| 5 | Training Module | 0% | ❌ Chưa bắt đầu |
| 6 | Application Module | 0% | ❌ Chưa bắt đầu |
| 7 | Comment & Extensions | 0% | ❌ Chưa bắt đầu |
| 8 | Theme Customization | 40% | 🔄 Cần hoàn thiện |

---

## 🚀 **ROADMAP TRIỂN KHAI TIẾP THEO**

### **🎯 CHIẾN LƯỢC: HOÀN THIỆN THEO ĐÚNG SEQUENCE**

**Tổng thời gian ước tính: 12 sessions (36 giờ) = 3 tuần**

---

## **PHASE 1: HOÀN THIỆN USER AUTHENTICATION** 
**⏱️ Timeline: 1 session (3 giờ)**
**🎯 Mục tiêu: Hoàn thiện 15% còn lại của PHASE 1**

### **Session 1.1: User Authentication Setup (3 giờ)**

#### **Bước 1.1.1: Enable OrchardCore.Users module (45 phút)**
- [ ] 1.1.1.1: Enable OrchardCore.Users trong Program.cs (15 phút)
- [ ] 1.1.1.2: Enable OrchardCore.Users.Registration (15 phút)
- [ ] 1.1.1.3: Cấu hình user settings trong appsettings.json (15 phút)

#### **Bước 1.1.2: Tạo user authentication views (45 phút)**
- [ ] 1.1.2.1: Tạo Views/Account/Register.liquid (15 phút)
- [ ] 1.1.2.2: Tạo Views/Account/Login.liquid (15 phút)
- [ ] 1.1.2.3: Tạo Views/Account/Profile.liquid (15 phút)

#### **Bước 1.1.3: Cấu hình user roles và permissions (45 phút)**
- [ ] 1.1.3.1: Tạo custom roles (Member, Premium, Admin) (15 phút)
- [ ] 1.1.3.2: Cấu hình role permissions (15 phút)
- [ ] 1.1.3.3: Tạo user registration workflow (15 phút)

#### **Bước 1.1.4: Tích hợp với theme hiện tại (45 phút)**
- [ ] 1.1.4.1: Cập nhật Layout.liquid với user menu (15 phút)
- [ ] 1.1.4.2: Tạo user login/logout components (15 phút)
- [ ] 1.1.4.3: Test user authentication flow (15 phút)

**✅ Kết quả mong đợi:**
- User có thể đăng ký, đăng nhập, đăng xuất
- Roles và permissions hoạt động đúng
- Theme hiển thị user menu

---

## **PHASE 2: TÁI CẤU TRÚC USER MANAGEMENT MODULE**
**⏱️ Timeline: 1 session (3 giờ)**
**🎯 Mục tiêu: Tái cấu trúc và hoàn thiện 30% còn lại**

### **Session 2.1: Module Restructuring (3 giờ)**

#### **Bước 2.1.1: Đổi tên module (45 phút)**
- [ ] 2.1.1.1: Rename HoangNgoc.UserExtensions → HoangNgoc.Users (15 phút)
- [ ] 2.1.1.2: Cập nhật namespace trong tất cả files (15 phút)
- [ ] 2.1.1.3: Cập nhật references và dependencies (15 phút)

#### **Bước 2.1.2: Tích hợp với OrchardCore.Users (45 phút)**
- [ ] 2.1.2.1: Cập nhật Manifest.cs dependencies (15 phút)
- [ ] 2.1.2.2: Tích hợp UserProfileExtPart với User entity (15 phút)
- [ ] 2.1.2.3: Cập nhật controllers để sử dụng OrchardCore.Users (15 phút)

#### **Bước 2.1.3: Tạo UserRegistrationService (45 phút)**
- [ ] 2.1.3.1: Tạo IUserRegistrationService interface (15 phút)
- [ ] 2.1.3.2: Implement UserRegistrationService.cs (15 phút)
- [ ] 2.1.3.3: Tích hợp với registration workflow (15 phút)

#### **Bước 2.1.4: Cập nhật views và controllers (45 phút)**
- [ ] 2.1.4.1: Cập nhật UserDashboardController (15 phút)
- [ ] 2.1.4.2: Cập nhật UserProfileController (15 phút)
- [ ] 2.1.4.3: Test tích hợp với authentication (15 phút)

**✅ Kết quả mong đợi:**
- Module có tên đúng: HoangNgoc.Users
- Tích hợp hoàn toàn với OrchardCore.Users
- User registration workflow hoạt động

---

## **PHASE 4: HOÀN THIỆN PAYMENT GATEWAY**
**⏱️ Timeline: 2 sessions (6 giờ)**
**🎯 Mục tiêu: Hoàn thiện 40% còn lại của payment system**

### **Session 4.1: VNPay Integration (3 giờ)**

#### **Bước 4.1.1: Tạo VNPay gateway (45 phút)**
- [ ] 4.1.1.1: Tạo VNPayGateway.cs implement IPaymentGateway (30 phút)
- [ ] 4.1.1.2: Cấu hình VNPay settings (15 phút)

#### **Bước 4.1.2: Implement VNPay API calls (45 phút)**
- [ ] 4.1.2.1: CreatePaymentUrl method (15 phút)
- [ ] 4.1.2.2: ProcessCallback method (15 phút)
- [ ] 4.1.2.3: ValidateSignature method (15 phút)

#### **Bước 4.1.3: Tạo VNPay webhook handler (45 phút)**
- [ ] 4.1.3.1: Tạo VNPayWebhookController.cs (30 phút)
- [ ] 4.1.3.2: Handle payment notifications (15 phút)

#### **Bước 4.1.4: Test VNPay integration (45 phút)**
- [ ] 4.1.4.1: Test payment creation (15 phút)
- [ ] 4.1.4.2: Test callback handling (15 phút)
- [ ] 4.1.4.3: Test error scenarios (15 phút)

### **Session 4.2: MoMo Integration & Security (3 giờ)**

#### **Bước 4.2.1: Tạo MoMo gateway (45 phút)**
- [ ] 4.2.1.1: Tạo MoMoGateway.cs implement IPaymentGateway (30 phút)
- [ ] 4.2.1.2: Cấu hình MoMo settings (15 phút)

#### **Bước 4.2.2: Implement MoMo API calls (45 phút)**
- [ ] 4.2.2.1: CreatePaymentRequest method (15 phút)
- [ ] 4.2.2.2: ProcessIPN method (15 phút)
- [ ] 4.2.2.3: ValidateSignature method (15 phút)

#### **Bước 4.2.3: Payment security & validation (45 phút)**
- [ ] 4.2.3.1: Implement payment encryption (15 phút)
- [ ] 4.2.3.2: Add request validation (15 phút)
- [ ] 4.2.3.3: Add fraud detection (15 phút)

#### **Bước 4.2.4: Hoàn thiện payment flow (45 phút)**
- [ ] 4.2.4.1: Cập nhật PurchaseController (15 phút)
- [ ] 4.2.4.2: Cập nhật payment views (15 phút)
- [ ] 4.2.4.3: Test end-to-end payment flow (15 phút)

**✅ Kết quả mong đợi:**
- VNPay và MoMo gateways hoạt động
- Payment security được đảm bảo
- End-to-end payment flow hoàn chỉnh

---

## **PHASE 5: TRAINING MODULE**
**⏱️ Timeline: 2 sessions (6 giờ)**
**🎯 Mục tiêu: Tạo module đào tạo từ 0 đến hoàn chỉnh**

### **Session 5.1: Training Module Foundation (3 giờ)**

#### **Bước 5.1.1: Tạo HoangNgoc.Training project (45 phút)**
- [ ] 5.1.1.1: Tạo HoangNgoc.Training.csproj (15 phút)
- [ ] 5.1.1.2: Tạo Manifest.cs (15 phút)
- [ ] 5.1.1.3: Tạo Startup.cs với training services (15 phút)

#### **Bước 5.1.2: Tạo training models (45 phút)**
- [ ] 5.1.2.1: Tạo CoursePart.cs (15 phút)
- [ ] 5.1.2.2: Tạo LessonPart.cs (15 phút)
- [ ] 5.1.2.3: Tạo EnrollmentModel.cs (15 phút)

#### **Bước 5.1.3: Tạo training services (45 phút)**
- [ ] 5.1.3.1: Tạo ICourseService và CourseService.cs (15 phút)
- [ ] 5.1.3.2: Tạo IEnrollmentService và EnrollmentService.cs (15 phút)
- [ ] 5.1.3.3: Tạo IProgressService và ProgressService.cs (15 phút)

#### **Bước 5.1.4: Tạo training controllers (45 phút)**
- [ ] 5.1.4.1: Tạo CourseController.cs (15 phút)
- [ ] 5.1.4.2: Tạo EnrollmentController.cs (15 phút)
- [ ] 5.1.4.3: Tạo training view models (15 phút)

### **Session 5.2: Training UI & Integration (3 giờ)**

#### **Bước 5.2.1: Tạo content part drivers (45 phút)**
- [ ] 5.2.1.1: Tạo CoursePartDriver.cs (30 phút)
- [ ] 5.2.1.2: Tạo LessonPartDriver.cs (15 phút)

#### **Bước 5.2.2: Tạo training views (45 phút)**
- [ ] 5.2.2.1: Tạo Course/Index.liquid (15 phút)
- [ ] 5.2.2.2: Tạo Course/Details.liquid (15 phút)
- [ ] 5.2.2.3: Tạo Enrollment/Enroll.liquid (15 phút)

#### **Bước 5.2.3: Tạo permissions và navigation (45 phút)**
- [ ] 5.2.3.1: Tạo TrainingPermissions.cs (15 phút)
- [ ] 5.2.3.2: Tạo TrainingNavigationProvider.cs (15 phút)
- [ ] 5.2.3.3: Tạo training-setup.recipe.json (15 phút)

#### **Bước 5.2.4: Integration với payment system (45 phút)**
- [ ] 5.2.4.1: Tích hợp course purchase với wallet (15 phút)
- [ ] 5.2.4.2: Tạo enrollment workflow (15 phút)
- [ ] 5.2.4.3: Test training module (15 phút)

**✅ Kết quả mong đợi:**
- Training module hoàn chỉnh
- Course management system
- Integration với payment và user system

---

## **PHASE 6: APPLICATION MODULE**
**⏱️ Timeline: 2 sessions (6 giờ)**
**🎯 Mục tiêu: Tạo module ứng dụng từ 0 đến hoàn chỉnh**

### **Session 6.1: Application Module Foundation (3 giờ)**

#### **Bước 6.1.1: Tạo HoangNgoc.Application project (45 phút)**
- [ ] 6.1.1.1: Tạo HoangNgoc.Application.csproj (15 phút)
- [ ] 6.1.1.2: Tạo Manifest.cs (15 phút)
- [ ] 6.1.1.3: Tạo Startup.cs (15 phút)

#### **Bước 6.1.2: Tạo application models (45 phút)**
- [ ] 6.1.2.1: Cập nhật Application.cs trong Core (15 phút)
- [ ] 6.1.2.2: Tạo ApplicationAccessPart.cs (15 phút)
- [ ] 6.1.2.3: Tạo UsageStatsPart.cs (15 phút)

#### **Bước 6.1.3: Tạo application services (45 phút)**
- [ ] 6.1.3.1: Cập nhật ApplicationService.cs (15 phút)
- [ ] 6.1.3.2: Tạo AccessControlService.cs (15 phút)
- [ ] 6.1.3.3: Tạo UsageTrackingService.cs (15 phút)

#### **Bước 6.1.4: Tạo controllers và view models (45 phút)**
- [ ] 6.1.4.1: Tạo ApplicationController.cs (15 phút)
- [ ] 6.1.4.2: Tạo AccessController.cs (15 phút)
- [ ] 6.1.4.3: Tạo application view models (15 phút)

### **Session 6.2: Application UI & Integration (3 giờ)**

#### **Bước 6.2.1: Tạo content part drivers (45 phút)**
- [ ] 6.2.1.1: Tạo ApplicationPartDriver.cs (30 phút)
- [ ] 6.2.1.2: Tạo AccessPartDriver.cs (15 phút)

#### **Bước 6.2.2: Tạo application views (45 phút)**
- [ ] 6.2.2.1: Tạo Application/Index.liquid (15 phút)
- [ ] 6.2.2.2: Tạo Application/Details.liquid (15 phút)
- [ ] 6.2.2.3: Tạo Access/Request.liquid (15 phút)

#### **Bước 6.2.3: Tạo permissions và GraphQL (45 phút)**
- [ ] 6.2.3.1: Tạo ApplicationPermissions.cs (15 phút)
- [ ] 6.2.3.2: Tạo ApplicationQueries.cs (15 phút)
- [ ] 6.2.3.3: Tạo application-setup.recipe.json (15 phút)

#### **Bước 6.2.4: Integration testing (45 phút)**
- [ ] 6.2.4.1: Test application access control (15 phút)
- [ ] 6.2.4.2: Integration với payment system (15 phút)
- [ ] 6.2.4.3: Test usage tracking (15 phút)

**✅ Kết quả mong đợi:**
- Application module hoàn chỉnh
- Access control system
- Usage tracking và analytics

---

## **PHASE 7: COMMENT & EXTENSIONS**
**⏱️ Timeline: 2 sessions (6 giờ)**
**🎯 Mục tiêu: Tạo comment system và extension services**

### **Session 7.1: Comment Module (3 giờ)**

#### **Bước 7.1.1: Tạo HoangNgoc.Comment project (45 phút)**
- [ ] 7.1.1.1: Tạo project structure (15 phút)
- [ ] 7.1.1.2: Tạo CommentPart.cs (15 phút)
- [ ] 7.1.1.3: Tạo CommentPartDriver.cs (15 phút)

#### **Bước 7.1.2: Tạo comment services (45 phút)**
- [ ] 7.1.2.1: Tạo ICommentService và CommentService.cs (30 phút)
- [ ] 7.1.2.2: Tạo comment moderation service (15 phút)

#### **Bước 7.1.3: Tạo comment controllers và views (45 phút)**
- [ ] 7.1.3.1: Tạo CommentController.cs (15 phút)
- [ ] 7.1.3.2: Tạo comment views (30 phút)

#### **Bước 7.1.4: Comment permissions và integration (45 phút)**
- [ ] 7.1.4.1: Tạo CommentPermissions.cs (15 phút)
- [ ] 7.1.4.2: Integration với user system (15 phút)
- [ ] 7.1.4.3: Test comment functionality (15 phút)

### **Session 7.2: Extensions & Notifications (3 giờ)**

#### **Bước 7.2.1: Tạo HoangNgoc.Extensions project (45 phút)**
- [ ] 7.2.1.1: Tạo project structure (15 phút)
- [ ] 7.2.1.2: Tạo EmailService.cs (15 phút)
- [ ] 7.2.1.3: Tạo SmsService.cs (15 phút)

#### **Bước 7.2.2: Tạo notification services (45 phút)**
- [ ] 7.2.2.1: Tạo INotificationService và NotificationService.cs (30 phút)
- [ ] 7.2.2.2: Tạo notification templates (15 phút)

#### **Bước 7.2.3: Tạo notification controllers và views (45 phút)**
- [ ] 7.2.3.1: Tạo NotificationController.cs (15 phút)
- [ ] 7.2.3.2: Tạo notification views (30 phút)

#### **Bước 7.2.4: Extensions integration (45 phút)**
- [ ] 7.2.4.1: Tạo ExtensionPermissions.cs (15 phút)
- [ ] 7.2.4.2: Integration với tất cả modules (15 phút)
- [ ] 7.2.4.3: Test notification system (15 phút)

**✅ Kết quả mong đợi:**
- Comment system hoàn chỉnh
- Email/SMS notification services
- Extension services tích hợp

---

## **PHASE 8: HOÀN THIỆN THEME CUSTOMIZATION**
**⏱️ Timeline: 2 sessions (6 giờ)**
**🎯 Mục tiêu: Hoàn thiện 60% còn lại của theme system**

### **Session 8.1: Advanced Theme Features (3 giờ)**

#### **Bước 8.1.1: Tạo advanced layout templates (45 phút)**
- [ ] 8.1.1.1: Tạo custom shape templates (15 phút)
- [ ] 8.1.1.2: Cập nhật Placement.json với advanced placement (15 phút)
- [ ] 8.1.1.3: Tạo theme-specific views (15 phút)

#### **Bước 8.1.2: Tạo responsive CSS (45 phút)**
- [ ] 8.1.2.1: Cập nhật hoangngoc.css với responsive design (30 phút)
- [ ] 8.1.2.2: Tạo admin.css cho admin customizations (15 phút)

#### **Bước 8.1.3: Tạo interactive JavaScript (45 phút)**
- [ ] 8.1.3.1: Cập nhật hoangngoc.js với interactive features (30 phút)
- [ ] 8.1.3.2: Tạo module-specific JavaScript (15 phút)

#### **Bước 8.1.4: Tạo content item views (45 phút)**
- [ ] 8.1.4.1: Tạo Items/Content.liquid (15 phút)
- [ ] 8.1.4.2: Tạo Items/Page.liquid (15 phút)
- [ ] 8.1.4.3: Tạo Shared/Error.liquid (15 phút)

### **Session 8.2: Final Integration & Testing (3 giờ)**

#### **Bước 8.2.1: Performance optimization (45 phút)**
- [ ] 8.2.1.1: Optimize database queries (15 phút)
- [ ] 8.2.1.2: Optimize CSS và JavaScript (15 phút)
- [ ] 8.2.1.3: Cấu hình caching strategies (15 phút)

#### **Bước 8.2.2: Security hardening (45 phút)**
- [ ] 8.2.2.1: Implement security headers (15 phút)
- [ ] 8.2.2.2: Validate input sanitization (15 phút)
- [ ] 8.2.2.3: Implement CSRF protection (15 phút)

#### **Bước 8.2.3: Comprehensive testing (45 phút)**
- [ ] 8.2.3.1: Integration tests cho tất cả modules (30 phút)
- [ ] 8.2.3.2: End-to-end testing (15 phút)

#### **Bước 8.2.4: Documentation & cleanup (45 phút)**
- [ ] 8.2.4.1: Code review và cleanup (15 phút)
- [ ] 8.2.4.2: Cập nhật XML documentation (15 phút)
- [ ] 8.2.4.3: Cập nhật README.md (15 phút)

**✅ Kết quả mong đợi:**
- Theme hoàn chỉnh với responsive design
- Performance được tối ưu
- Security được đảm bảo
- Documentation đầy đủ

---

## 📋 **TỔNG KẾT ROADMAP**

### **🎯 TIMELINE TỔNG QUAN:**
- **Phase 1**: 1 session (3 giờ) - Hoàn thiện User Authentication
- **Phase 2**: 1 session (3 giờ) - Tái cấu trúc User Management
- **Phase 4**: 2 sessions (6 giờ) - Hoàn thiện Payment Gateway
- **Phase 5**: 2 sessions (6 giờ) - Training Module
- **Phase 6**: 2 sessions (6 giờ) - Application Module
- **Phase 7**: 2 sessions (6 giờ) - Comment & Extensions
- **Phase 8**: 2 sessions (6 giờ) - Theme Customization

**📊 TỔNG CỘNG: 12 sessions = 36 giờ = 3 tuần**

### **🎯 MỤC TIÊU CUỐI CÙNG:**
- ✅ Hệ thống OrchardCore hoàn chỉnh với 6 modules
- ✅ User authentication & management system
- ✅ Wallet & payment system với VNPay, MoMo
- ✅ Training & course management
- ✅ Application access control
- ✅ Comment & notification system
- ✅ Professional responsive theme
- ✅ Security & performance optimized

### **🚀 BƯỚC TIẾP THEO:**
**Sẵn sàng bắt đầu PHASE 1 - Session 1.1: User Authentication Setup!**

---

*Tài liệu này sẽ được cập nhật theo tiến độ thực tế của dự án.*