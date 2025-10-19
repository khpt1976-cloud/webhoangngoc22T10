# 📋 **TEST CASES TEMPLATE**
## **HoangNgoc Theme v2.0 - Detailed Test Cases**

---

## 🎨 **THEME UI TEST CASES**

### **TC-UI-001: Header Navigation**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-UI-001 |
| **Module** | Theme Layout |
| **Priority** | High |
| **Type** | Functional |
| **Precondition** | Application running, theme activated |

**Test Steps:**
1. Navigate to homepage
2. Verify logo displays correctly
3. Check main navigation menu items
4. Test dropdown menus
5. Verify user menu (login/logout)
6. Test mobile hamburger menu

**Expected Results:**
- Logo displays in correct position
- All navigation links functional
- Dropdown menus work smoothly
- User menu shows appropriate options
- Mobile menu responsive

**Pass Criteria:** All navigation elements functional ✅

---

### **TC-UI-002: Responsive Design**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-UI-002 |
| **Module** | Theme Responsive |
| **Priority** | High |
| **Type** | UI/UX |
| **Precondition** | Theme activated, browser DevTools available |

**Test Steps:**
1. Open browser DevTools
2. Test mobile view (375px width)
3. Test tablet view (768px width)
4. Test desktop view (1200px width)
5. Test large desktop (1440px width)
6. Verify touch targets on mobile

**Expected Results:**
- Layout adapts to all screen sizes
- Content readable on all devices
- Touch targets minimum 44px
- No horizontal scrolling
- Images scale appropriately

**Pass Criteria:** Perfect responsive behavior ✅

---

### **TC-UI-003: CSS Animations**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-UI-003 |
| **Module** | Theme Animations |
| **Priority** | Medium |
| **Type** | Visual |
| **Precondition** | Theme with animations loaded |

**Test Steps:**
1. Navigate to homepage
2. Observe float animations
3. Test hover effects on buttons
4. Check scroll reveal animations
5. Verify loading states
6. Test reduced motion preference

**Expected Results:**
- Animations smooth và performant
- Hover effects responsive
- Scroll animations trigger correctly
- Loading states visible
- Reduced motion respected

**Pass Criteria:** All animations working smoothly ✅

---

## 🔐 **AUTHENTICATION MODULE TEST CASES**

### **TC-AUTH-001: User Registration**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-AUTH-001 |
| **Module** | HoangNgoc.Authentication |
| **Priority** | High |
| **Type** | Functional |
| **Precondition** | Registration enabled |

**Test Steps:**
1. Navigate to /register
2. Fill registration form:
   - Email: test@example.com
   - Password: SecurePass123!
   - Confirm Password: SecurePass123!
   - Accept Terms: ✓
3. Submit form
4. Check email verification
5. Verify account creation

**Expected Results:**
- Form validation works
- Email verification sent
- Account created successfully
- User can login after verification

**Pass Criteria:** Complete registration flow ✅

---

### **TC-AUTH-002: Login Process**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-AUTH-002 |
| **Module** | HoangNgoc.Authentication |
| **Priority** | High |
| **Type** | Functional |
| **Precondition** | Valid user account exists |

**Test Steps:**
1. Navigate to /login
2. Enter valid credentials
3. Click Login button
4. Verify successful login
5. Check user menu display
6. Test logout functionality

**Expected Results:**
- Login form accepts credentials
- Successful login redirects appropriately
- User menu shows logged-in state
- Logout clears session

**Pass Criteria:** Login/logout cycle complete ✅

---

### **TC-AUTH-003: Password Reset**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-AUTH-003 |
| **Module** | HoangNgoc.Authentication |
| **Priority** | Medium |
| **Type** | Functional |
| **Precondition** | User account exists |

**Test Steps:**
1. Navigate to /login
2. Click "Forgot Password"
3. Enter email address
4. Submit reset request
5. Check email for reset link
6. Follow reset link
7. Set new password
8. Login with new password

**Expected Results:**
- Reset email sent successfully
- Reset link valid và secure
- New password accepted
- Login works with new password

**Pass Criteria:** Password reset flow complete ✅

---

## 💰 **CORE MODULE TEST CASES**

### **TC-CORE-001: Wallet Creation**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-CORE-001 |
| **Module** | HoangNgoc.Core |
| **Priority** | High |
| **Type** | Functional |
| **Precondition** | User registered và logged in |

**Test Steps:**
1. Login as new user
2. Navigate to wallet section
3. Verify wallet auto-creation
4. Check initial balance (0)
5. Verify wallet ID generation
6. Test wallet display

**Expected Results:**
- Wallet created automatically
- Initial balance is 0
- Unique wallet ID assigned
- Wallet displays correctly

**Pass Criteria:** Wallet creation successful ✅

---

### **TC-CORE-002: Wallet Top-up**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-CORE-002 |
| **Module** | HoangNgoc.Core |
| **Priority** | High |
| **Type** | Functional |
| **Precondition** | User has wallet |

**Test Steps:**
1. Navigate to wallet page
2. Click "Top-up" button
3. Select payment method (VNPay)
4. Enter amount: 100,000 VND
5. Complete payment flow
6. Verify balance update
7. Check transaction history

**Expected Results:**
- Top-up form displays correctly
- Payment gateway integration works
- Balance updates after payment
- Transaction recorded in history

**Pass Criteria:** Top-up process complete ✅

---

### **TC-CORE-003: User Transfer**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-CORE-003 |
| **Module** | HoangNgoc.Core |
| **Priority** | High |
| **Type** | Functional |
| **Precondition** | User has sufficient balance |

**Test Steps:**
1. Navigate to transfer page
2. Enter recipient email
3. Enter transfer amount: 50,000 VND
4. Add transfer message
5. Confirm transaction
6. Verify sender balance decreased
7. Verify recipient balance increased
8. Check both transaction histories

**Expected Results:**
- Transfer form validation works
- Both balances update correctly
- Transaction recorded for both users
- Notifications sent appropriately

**Pass Criteria:** Transfer process complete ✅

---

## 📰 **NEWS MODULE TEST CASES**

### **TC-NEWS-001: Create Article**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-NEWS-001 |
| **Module** | HoangNgoc.News |
| **Priority** | High |
| **Type** | Functional |
| **Precondition** | Admin/Editor logged in |

**Test Steps:**
1. Navigate to News Admin
2. Click "Create Article"
3. Fill article details:
   - Title: "Test News Article"
   - Content: Rich text content
   - Category: Technology
   - Tags: test, news
   - Featured Image: Upload image
4. Set publication status: Published
5. Save article
6. Verify frontend display

**Expected Results:**
- Article creation form works
- Rich text editor functional
- Image upload successful
- Article displays on frontend
- SEO meta tags generated

**Pass Criteria:** Article creation complete ✅

---

### **TC-NEWS-002: News Listing**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-NEWS-002 |
| **Module** | HoangNgoc.News |
| **Priority** | Medium |
| **Type** | Functional |
| **Precondition** | Published articles exist |

**Test Steps:**
1. Navigate to /news
2. Verify articles display
3. Test pagination
4. Filter by category
5. Search for articles
6. Test article links

**Expected Results:**
- Articles display in grid/list
- Pagination works correctly
- Category filtering functional
- Search returns relevant results
- Article links navigate correctly

**Pass Criteria:** News listing functional ✅

---

## 💬 **COMMENT MODULE TEST CASES**

### **TC-COMMENT-001: Add Comment**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-COMMENT-001 |
| **Module** | HoangNgoc.Comment |
| **Priority** | Medium |
| **Type** | Functional |
| **Precondition** | User logged in, article exists |

**Test Steps:**
1. Navigate to news article
2. Scroll to comment section
3. Enter comment text
4. Submit comment
5. Verify comment appears
6. Check notification sent

**Expected Results:**
- Comment form displays
- Comment submission successful
- Comment appears immediately
- Author receives notification

**Pass Criteria:** Comment system functional ✅

---

## 💳 **PAYMENT MODULE TEST CASES**

### **TC-PAYMENT-001: VNPay Integration**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-PAYMENT-001 |
| **Module** | HoangNgoc.Payment |
| **Priority** | High |
| **Type** | Integration |
| **Precondition** | VNPay configured |

**Test Steps:**
1. Select item to purchase
2. Choose VNPay payment
3. Enter payment details
4. Complete VNPay flow
5. Return to website
6. Verify payment success
7. Check order status

**Expected Results:**
- VNPay gateway loads correctly
- Payment process smooth
- Success confirmation received
- Order status updated

**Pass Criteria:** VNPay integration working ✅

---

## 🎓 **TRAINING MODULE TEST CASES**

### **TC-TRAINING-001: Course Enrollment**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-TRAINING-001 |
| **Module** | HoangNgoc.Training |
| **Priority** | High |
| **Type** | Functional |
| **Precondition** | Course exists, user logged in |

**Test Steps:**
1. Browse course catalog
2. Select course
3. Click "Enroll Now"
4. Complete payment (if required)
5. Access course content
6. Verify enrollment status

**Expected Results:**
- Course details display correctly
- Enrollment process smooth
- Payment integration works
- Course content accessible
- Progress tracking enabled

**Pass Criteria:** Course enrollment complete ✅

---

## 💼 **APPLICATION MODULE TEST CASES**

### **TC-APP-001: Job Application**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-APP-001 |
| **Module** | HoangNgoc.Application |
| **Priority** | High |
| **Type** | Functional |
| **Precondition** | Job posting exists |

**Test Steps:**
1. Browse job listings
2. Select job position
3. Fill application form
4. Upload resume/CV
5. Submit application
6. Verify confirmation
7. Check application status

**Expected Results:**
- Job listings display correctly
- Application form functional
- File upload works
- Confirmation email sent
- Application tracked in system

**Pass Criteria:** Job application complete ✅

---

## 🔗 **INTEGRATION TEST CASES**

### **TC-INT-001: Complete User Journey**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-INT-001 |
| **Module** | All Modules |
| **Priority** | High |
| **Type** | End-to-End |
| **Precondition** | All modules activated |

**Test Steps:**
1. Register new account
2. Complete user profile
3. Top-up wallet
4. Enroll in course
5. Complete training
6. Apply for job
7. Make payment
8. Add comments
9. Read news
10. Verify data consistency

**Expected Results:**
- All steps complete successfully
- Data consistent across modules
- User experience smooth
- No integration conflicts

**Pass Criteria:** Complete journey successful ✅

---

## ⚡ **PERFORMANCE TEST CASES**

### **TC-PERF-001: Core Web Vitals**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-PERF-001 |
| **Module** | Theme Performance |
| **Priority** | High |
| **Type** | Performance |
| **Precondition** | Production-like environment |

**Test Steps:**
1. Open Chrome DevTools
2. Navigate to Lighthouse tab
3. Run performance audit
4. Check Core Web Vitals:
   - LCP (Largest Contentful Paint)
   - FID (First Input Delay)
   - CLS (Cumulative Layout Shift)
5. Verify scores

**Expected Results:**
- LCP < 2.5 seconds
- FID < 100 milliseconds
- CLS < 0.1
- Overall score > 90

**Pass Criteria:** All metrics in green zone ✅

---

## 🔒 **SECURITY TEST CASES**

### **TC-SEC-001: XSS Prevention**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-SEC-001 |
| **Module** | All Input Forms |
| **Priority** | High |
| **Type** | Security |
| **Precondition** | Application running |

**Test Steps:**
1. Navigate to comment form
2. Enter XSS payload: `<script>alert('XSS')</script>`
3. Submit form
4. Verify payload sanitized
5. Check no script execution
6. Test other input forms

**Expected Results:**
- XSS payload sanitized
- No script execution
- Content displayed safely
- Security headers present

**Pass Criteria:** XSS prevention effective ✅

---

## 📱 **MOBILE TEST CASES**

### **TC-MOB-001: Mobile Navigation**
| **Field** | **Details** |
|-----------|-------------|
| **Test ID** | TC-MOB-001 |
| **Module** | Theme Mobile |
| **Priority** | High |
| **Type** | Mobile UI |
| **Precondition** | Mobile device or emulator |

**Test Steps:**
1. Open site on mobile device
2. Test hamburger menu
3. Navigate through pages
4. Test touch interactions
5. Verify responsive layout
6. Check touch target sizes

**Expected Results:**
- Hamburger menu functional
- Navigation smooth
- Touch targets ≥ 44px
- Layout responsive
- Content readable

**Pass Criteria:** Excellent mobile experience ✅

---

## 📊 **TEST EXECUTION TRACKING**

### **Test Execution Log Template:**
```
Date: ___________
Tester: ___________
Environment: ___________
Browser: ___________
Device: ___________

Test Results:
□ TC-UI-001: Header Navigation - PASS/FAIL
□ TC-UI-002: Responsive Design - PASS/FAIL
□ TC-UI-003: CSS Animations - PASS/FAIL
□ TC-AUTH-001: User Registration - PASS/FAIL
□ TC-AUTH-002: Login Process - PASS/FAIL
□ TC-CORE-001: Wallet Creation - PASS/FAIL
□ TC-NEWS-001: Create Article - PASS/FAIL
□ TC-COMMENT-001: Add Comment - PASS/FAIL
□ TC-PAYMENT-001: VNPay Integration - PASS/FAIL
□ TC-TRAINING-001: Course Enrollment - PASS/FAIL
□ TC-APP-001: Job Application - PASS/FAIL
□ TC-INT-001: Complete User Journey - PASS/FAIL
□ TC-PERF-001: Core Web Vitals - PASS/FAIL
□ TC-SEC-001: XSS Prevention - PASS/FAIL
□ TC-MOB-001: Mobile Navigation - PASS/FAIL

Overall Status: PASS/FAIL
Notes: ___________
```

---

## 🐛 **BUG REPORT TEMPLATE**

### **Bug Report Format:**
```
Bug ID: BUG-001
Date: ___________
Reporter: ___________
Module: ___________
Priority: High/Medium/Low
Severity: Critical/Major/Minor

Summary: Brief description of the bug

Steps to Reproduce:
1. Step one
2. Step two
3. Step three

Expected Result:
What should happen

Actual Result:
What actually happened

Environment:
- Browser: ___________
- OS: ___________
- Device: ___________
- Screen Resolution: ___________

Screenshots/Videos:
[Attach if applicable]

Additional Notes:
Any other relevant information
```

---

**📋 Test Cases Created**: 2025-10-19  
**👨‍💻 Created By**: HoangNgoc QA Team  
**🎯 Coverage**: All 8 modules + Theme + Integration  
**📊 Total Test Cases**: 15+ comprehensive scenarios  

---

**🧪 Professional Testing Standards for Production Excellence** ✨