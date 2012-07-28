namespace __NAME__.Domain
{
    using System;

    public abstract class BaseDomainObject<T> : IDomainObject
    {
        public T Id { get; set; }

        // auditing fields
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public object GetKey()
        {
            return Id;
        }

        /// <summary>
        /// Sets the properties before insert. This is only called when this item is first being created
        /// </summary>
        public virtual void SetInitialInsertProperties()
        {
            CreateDate = DateTime.Now;
        }  
        
        /// <summary>
        /// Sets the properties before update. This is called every time this item is saved
        /// </summary>
        public virtual void SetUpdateProperties()
        {
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }
    }
}