using ImageAIService.Models;
using System.Collections.Generic; // لإضافة List

namespace ImageAIService.Models // 🚩 تم التصحيح: Models بدلاً من Modles
{
    // نفترض أن BaseEntity موجودة في نفس الـ Namespace
    public class Image : BaseEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string StoragePath { get; set; }
        public string Status { get; set; }

        // ------------------------------------
        // علاقة المستخدم (One-to-Many: User uploads many Images)
        // ------------------------------------
        public int UserId { get; set; } // المفتاح الخارجي (Foreign Key)

        public User User { get; set; } // خاصية مرجعية (Navigation Property)

        // ------------------------------------
        // علاقة الوسوم (One-to-Many: Image has many Tags)
        // ------------------------------------
        public List<Tag> Tags { get; set; } = new List<Tag>(); // خاصية مرجعية
    }
}