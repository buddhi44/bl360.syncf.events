namespace bl360.domain
{
    public class Class1
    {

    }

    public class BaseEntity
    {
        public byte IsActive { get; set; } = 1;
        public byte IsApproved { get; set; } = 1;
        public bool IsPersisted { get; set; }
        public bool IsDirty { get; set; }
        public bool IsMust { get; set; }
        public int RequestingObjectKey { get; set; } = 1;
        public IDictionary<string, object> AddtionalData { get; set; }

        public BaseEntity()
        {
            AddtionalData = new Dictionary<string, object>();

        }

    }
}
