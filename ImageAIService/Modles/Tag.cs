
namespace ImageAIService.Models // 🚩 تم التصحيح
{
    public class Tag : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Confidence { get; set; }

        public int ImageId { get; set; }

        public Image Image { get; set; }
    }
}