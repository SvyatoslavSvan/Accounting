namespace Accounting.Domain.Enums
{
    public enum DocumentType
    {
        Accrual,
        Deducation
    }

    public static class DocumentTypeExtensions
    {
        public static string ToFriendlyString(this DocumentType type)
        {
            switch (type)
            {
                case DocumentType.Accrual:
                    return "Начисление";
                case DocumentType.Deducation:
                    return "Отчисление";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

}
