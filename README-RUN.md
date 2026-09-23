# 🥇 گلدن‌شک — راهنمای اجرای محلی روی سیستم خودتان

این پکیج شامل **بک‌اند ASP.NET Core 6** و **فرانت‌اند Angular 18** است.

```
GoldenCheque-full
├── GoldenChequeBack.Domain/         ← موجودیت‌ها
├── GoldenChequeBack.Persistence/    ← EF Core + Migration‌ها
├── GoldenChequeBack.Service/        ← سرویس‌ها و Repository‌ها
├── GoldenChequeBack.Infrastructure/ ← Middleware و Extension‌ها
├── GoldenChqeueBack/                ← پروژه اصلی Web API
├── golden-cheque-ui/                ← فرانت‌اند Angular (RTL فارسی)
├── GoldenChqeueBack.sln
└── README-RUN.md                    ← همین راهنما
```

---

## ۱) پیش‌نیازها

| ابزار | نسخه | لینک |
|---|---|---|
| .NET SDK | 6.0+ | https://dotnet.microsoft.com/download/dotnet/6.0 |
| Node.js | 18+ | https://nodejs.org |
| SQL Server | 2019+ (یا LocalDB/Express) | https://www.microsoft.com/sql-server |

---

## ۲) تنظیمات و ساخت خودکار دیتابیس

الف) مقادیر اتصال را **بدون کامیت در گیت** از طریق فایل `GoldenChqeueBack/appsettings.Development.json` بدهید (این فایل را به .gitignore اضافه کنید):

```json
{
  "ConnectionStrings": {
    "OnionArchConn": "Data Source=.\\SQLEXPRESS;Initial Catalog=GoldenCheque;Trusted_Connection=True;TrustServerCertificate=True",
    "IdentityConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=GoldenCheque;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "یک-کلید-تصادفی-حداقل-۳۲-کاراکتر-اینجا-بگذارید",
    "Issuer": "Identity",
    "Audience": "IdentityUser",
    "DurationInMinutes": 60
  }
}
```

ب) همین!

از نسخه فعلی، برنامه هنگام اجرا به‌صورت **خودکار** دیتابیس‌ها را می‌سازد:
- اگر دیتابیس وجود نداشته باشد، ساخته می‌شود (`Database.Migrate` روی هر دو کانتکست).
- مایگریشن‌ها اعمال و دیتای Seed (نقش‌ها، کاربر superadmin/basicuser، واحد «بسته» و دسته‌بندی‌ها) درج می‌شود.
- اگر خطای اتصال رخ دهد، برنامه با پیام واضح لاگ متوقف می‌شود تا مشکل connection string رفع شود.

پیش‌نیاز: فقط SQL Server باید در حال اجرا باشد؛ لازم نیست دیتابیس خالی بسازید.

اگر خواستید مایگریشن‌ها را دستی اعمال کنید (مثلاً برای عیب‌یابی):

```bash
dotnet ef database update --project GoldenChequeBack.Persistence --startup-project GoldenChqeueBack --context ApplicationDbContext
dotnet ef database update --project GoldenChequeBack.Persistence --startup-project GoldenChqeueBack --context IdentityContext
```

> اگر ابزار ef ندارید: `dotnet tool install --global dotnet-ef`

دیتای Seed پیش‌فرض:
- کاربر: `superadmin@gmail.com` / رمز: `Admin@12345`
- واحد پیش‌فرض «بسته» و دسته‌بندی‌های «الکترونیکی/غذایی»

---

## ۳) اجرای بک‌اند

```bash
dotnet run --project GoldenChqeueBack
```

بک‌اند روی `http://localhost:5082` بالا می‌آید.
Swagger (در حالت Development): `http://localhost:5082/OpenAPI`

---

## ۴) اجرای فرانت‌اند

در یک ترمینال جدید:

```bash
cd golden-cheque-ui
npm install
npm start
```

فرانت روی `http://localhost:4200` بالا می‌آید و از طریق proxy خودکار به بک‌اند روی 5082 وصل می‌شود.

ورود: `superadmin@gmail.com` / `Admin@12345`

---

## ۵) نکات مهم امنیتی ⚠️

- **هرگز** connection string و کلید JWT را در `appsettings.json` کامیت نکنید (قبلاً از این فایل حذف شده‌اند).
- App Password جیمیل قبلاً در گیت لو رفته — اگر استفاده می‌کنید آن را **لغو** کنید و از Gmail → App Passwords یکی جدید بسازید و فقط به‌صورت متغیر محیطی `MailSettings__SmtpPass` بدهید.
- تأیید ایمیل پیش‌فرض خاموش است (`FeatureManagement:EnableEmailService = false`) — کاربران بدون ایمیل هم با لینک فعال‌سازی مستقیم ساخته می‌شوند.

---

## عیب‌یابی سریع

| مشکل | راه‌حل |
|---|---|
| `403 Forbidden` موقع باز کردن لینک preview | مخصوص لینک ابری است؛ روی localhost چنین مشکلی نیست |
| خطای اتصال SQL | connection string را در `appsettings.Development.json` چک کنید؛ `TrustServerCertificate=True` را فراموش نکنید |
| پورت 5082 اشغال | در `Properties/launchSettings.json` تغییر دهید و `proxy.conf.json` فرانت را هم همان کنید |
| فونت وزیرمتن لود نمی‌شود (آفلاین) | اینترنت برای Google Fonts لازم است؛ یا فونت را محلی کنید |
