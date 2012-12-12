using System;
using System.ComponentModel.DataAnnotations;

namespace __NAME__.Domain
{
    public class SampleUser : BaseDomainObject<long>
    {
        [MaxLength(255)]
        public string UserName { get; set; }
        public Guid UserKey { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string FirstName { get; set; }
        [MaxLength(255)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Suffix { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime? LastLoginDate { get; set; }
        
        public override void SetInitialInsertProperties()
        {
            base.SetInitialInsertProperties();
            UserKey = Guid.NewGuid();
            CreateDate = DateTime.Now;
            IsActive = false;
            IsLockedOut = false;
        }
    }
}