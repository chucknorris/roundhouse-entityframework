namespace __NAME__.Domain
{
    public interface IDomainObject
    {
        object GetKey();
        void SetInitialInsertProperties();
        void SetUpdateProperties();
    }
}