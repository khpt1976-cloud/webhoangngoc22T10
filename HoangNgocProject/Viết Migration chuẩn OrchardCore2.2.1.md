# 📚 Hướng dẫn viết Migration chuẩn cho OrchardCore

## 🎯 Tổng quan
Migration trong OrchardCore là cách tự động tạo và quản lý Content Types, Content Parts, và cấu trúc database. Việc viết migration đúng cách giúp:
- Tự động tạo content types khi enable module
- Đảm bảo tính nhất quán giữa các môi trường
- Dễ dàng triển khai và bảo trì
- Tái sử dụng module trong các dự án khác

## ⚠️ Lỗi thường gặp khi viết Migration

### 1. **Sử dụng Synchronous thay vì Async (SAI)**
```csharp
// ❌ SAI - Cách cũ, không hoạt động với OrchardCore v2.x
public int Create()
{
    _contentDefinitionManager.AlterTypeDefinition("Course", type => type
        .WithPart("TitlePart")
        .Creatable()
    );
    return 1;
}
```

### 2. **Thiếu async/await pattern (SAI)**
```csharp
// ❌ SAI - Method không async
public int CreateAsync()
{
    _contentDefinitionManager.AlterTypeDefinitionAsync("Course", type => type
        .WithPart("TitlePart")
        .Creatable()
    );
    return 1;
}
```

### 3. **Không sử dụng proper positioning (SAI)**
```csharp
// ❌ SAI - Không có position, layout sẽ không đúng
.WithPart("TitlePart")
.WithPart("HtmlBodyPart")
```

## ✅ Cách viết Migration đúng chuẩn

### 1. **Cấu trúc file Migration**
```csharp
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Autoroute.Models;

namespace HoangNgoc.Training.Migrations
{
    public class TrainingMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public TrainingMigrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        // Migration đầu tiên - luôn là CreateAsync()
        public async Task<int> CreateAsync()
        {
            // Tạo content types ở đây
            return 1;
        }

        // Migration thứ 2 - UpdateFrom1Async()
        public async Task<int> UpdateFrom1Async()
        {
            // Cập nhật hoặc thêm content types
            return 2;
        }

        // Migration thứ 3 - UpdateFrom2Async()
        public async Task<int> UpdateFrom2Async()
        {
            // Tiếp tục cập nhật
            return 3;
        }
    }
}
```

### 2. **Template tạo Content Type chuẩn**
```csharp
public async Task<int> CreateAsync()
{
    // Tạo Course content type
    await _contentDefinitionManager.AlterTypeDefinitionAsync("Course", type => type
        .WithPart("TitlePart", part => part
            .WithPosition("0")  // ✅ Luôn có position
        )
        .WithPart("AutoroutePart", part => part
            .WithPosition("1")
            .WithSettings(new AutoroutePartSettings
            {
                Pattern = "courses/{{ Model.ContentItem | display_text | slugify }}",
                AllowCustomPath = true
            })
        )
        .WithPart("HtmlBodyPart", part => part
            .WithPosition("2")
        )
        .WithPart("CommonPart", part => part
            .WithPosition("3")
        )
        .Creatable()    // ✅ Có thể tạo mới
        .Listable()     // ✅ Hiển thị trong danh sách
        .Draftable()    // ✅ Có thể lưu draft
        .Versionable()  // ✅ Có version control
        .Securable()    // ✅ Có security
    );

    return 1;
}
```

### 3. **Các loại Content Type thường dùng**

#### **A. Content Type có URL riêng (với AutoroutePart)**
```csharp
await _contentDefinitionManager.AlterTypeDefinitionAsync("Course", type => type
    .WithPart("TitlePart", part => part.WithPosition("0"))
    .WithPart("AutoroutePart", part => part
        .WithPosition("1")
        .WithSettings(new AutoroutePartSettings
        {
            Pattern = "courses/{{ Model.ContentItem | display_text | slugify }}",
            AllowCustomPath = true
        })
    )
    .WithPart("HtmlBodyPart", part => part.WithPosition("2"))
    .WithPart("CommonPart", part => part.WithPosition("3"))
    .Creatable().Listable().Draftable().Versionable().Securable()
);
```

#### **B. Content Type đơn giản (không cần URL)**
```csharp
await _contentDefinitionManager.AlterTypeDefinitionAsync("Enrollment", type => type
    .WithPart("TitlePart", part => part.WithPosition("0"))
    .WithPart("CommonPart", part => part.WithPosition("1"))
    .Creatable().Listable().Draftable().Versionable().Securable()
);
```

#### **C. Content Type cho Category/Taxonomy**
```csharp
await _contentDefinitionManager.AlterTypeDefinitionAsync("CourseCategory", type => type
    .WithPart("TitlePart", part => part.WithPosition("0"))
    .WithPart("AutoroutePart", part => part
        .WithPosition("1")
        .WithSettings(new AutoroutePartSettings
        {
            Pattern = "course-categories/{{ Model.ContentItem | display_text | slugify }}",
            AllowCustomPath = true
        })
    )
    .WithPart("HtmlBodyPart", part => part.WithPosition("2"))
    .WithPart("CommonPart", part => part.WithPosition("3"))
    .Creatable().Listable().Draftable().Versionable()  // Không cần Securable cho category
);
```

### 4. **Ví dụ Migration hoàn chỉnh**
```csharp
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Autoroute.Models;

namespace HoangNgoc.Training.Migrations
{
    public class TrainingMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public TrainingMigrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            // Tạo Course content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Course", type => type
                .WithPart("TitlePart", part => part.WithPosition("0"))
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "courses/{{ Model.ContentItem | display_text | slugify }}",
                        AllowCustomPath = true
                    })
                )
                .WithPart("HtmlBodyPart", part => part.WithPosition("2"))
                .WithPart("CommonPart", part => part.WithPosition("3"))
                .Creatable().Listable().Draftable().Versionable().Securable()
            );

            // Tạo Lesson content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Lesson", type => type
                .WithPart("TitlePart", part => part.WithPosition("0"))
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "lessons/{{ Model.ContentItem | display_text | slugify }}",
                        AllowCustomPath = true
                    })
                )
                .WithPart("HtmlBodyPart", part => part.WithPosition("2"))
                .WithPart("CommonPart", part => part.WithPosition("3"))
                .Creatable().Listable().Draftable().Versionable().Securable()
            );

            return 1;
        }

        public async Task<int> UpdateFrom1Async()
        {
            // Thêm Enrollment content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Enrollment", type => type
                .WithPart("TitlePart", part => part.WithPosition("0"))
                .WithPart("CommonPart", part => part.WithPosition("1"))
                .Creatable().Listable().Draftable().Versionable().Securable()
            );

            return 2;
        }
    }
}
```

## 🔧 Các Parts thường dùng

### **Core Parts (Built-in OrchardCore)**
- `TitlePart` - Tiêu đề content
- `HtmlBodyPart` - Nội dung HTML
- `CommonPart` - Thông tin chung (CreatedUtc, ModifiedUtc, Owner, etc.)
- `AutoroutePart` - Tạo URL thân thiện
- `PublishLaterPart` - Lên lịch publish
- `FlowPart` - Tạo layout phức tạp

### **Custom Parts (Tự định nghĩa)**
- `CoursePart` - Thông tin khóa học
- `JobPostingPart` - Thông tin tuyển dụng
- `NewsArticlePart` - Thông tin bài viết

## 📋 Checklist khi viết Migration

### ✅ **Bắt buộc phải có:**
- [ ] Sử dụng `async Task<int>` cho tất cả migration methods
- [ ] Sử dụng `await _contentDefinitionManager.AlterTypeDefinitionAsync()`
- [ ] Đặt `WithPosition()` cho tất cả parts
- [ ] Có `TitlePart` ở position "0"
- [ ] Có `CommonPart` ở position cuối cùng
- [ ] Có `.Creatable().Listable().Draftable().Versionable()`
- [ ] Return đúng version number

### ✅ **Nên có:**
- [ ] `AutoroutePart` cho content có URL riêng
- [ ] `HtmlBodyPart` cho content có nội dung
- [ ] `.Securable()` cho content cần bảo mật
- [ ] Pattern URL có ý nghĩa và SEO-friendly

### ✅ **Tùy chọn:**
- [ ] Custom Parts nếu cần thêm fields
- [ ] `PublishLaterPart` nếu cần lên lịch
- [ ] `FlowPart` nếu cần layout phức tạp

## 🚀 Cách test Migration

### 1. **Build và chạy application**
```bash
dotnet build
dotnet run --urls="http://0.0.0.0:5000"
```

### 2. **Kiểm tra Content Types**
- Truy cập `/Admin/ContentTypes/List`
- Xem các content types đã được tạo
- Test tạo content mới

### 3. **Kiểm tra logs**
```bash
grep -i "migration\|error" server.log
```

## 📁 Cấu trúc thư mục Migration

```
src/HoangNgoc.Modules/HoangNgoc.Training/
├── Migrations/
│   └── TrainingMigrations.cs     ✅ Đúng vị trí
├── Startup.cs                    ✅ Đăng ký services
└── Manifest.cs                   ✅ Module info
```

## 🎯 Best Practices

### 1. **Naming Convention**
- Migration class: `{ModuleName}Migrations`
- Content Type: PascalCase (`Course`, `NewsArticle`)
- URL Pattern: kebab-case (`courses`, `news-articles`)

### 2. **Version Management**
- `CreateAsync()` return 1
- `UpdateFrom1Async()` return 2
- `UpdateFrom2Async()` return 3
- Luôn tăng version number

### 3. **Error Handling**
- Migration sẽ tự động rollback nếu có lỗi
- Luôn test migration trên môi trường dev trước
- Backup database trước khi chạy migration production

## 🔍 Troubleshooting

### **Migration không chạy:**
- Kiểm tra Startup.cs đã đăng ký migration chưa
- Kiểm tra namespace và class name
- Xem logs có lỗi compilation không

### **Content Type không xuất hiện:**
- Kiểm tra method name: `CreateAsync()`, `UpdateFrom1Async()`
- Kiểm tra return value đúng version
- Restart application sau khi sửa migration

### **Layout không đúng:**
- Kiểm tra `WithPosition()` cho tất cả parts
- TitlePart luôn ở position "0"
- CommonPart luôn ở position cuối

---

**📝 Lưu ý:** Migration chỉ chạy một lần khi enable module hoặc khi có version mới. Nếu muốn chạy lại, cần disable/enable module hoặc tăng version number.