namespace __NAME__.Domain
{
    public class SampleChildObject : BaseDomainObject<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual SampleParentObject ParentObject { get; set; }
        public long ParentObjectId { get; set; }
    }
}