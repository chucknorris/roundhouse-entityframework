using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace __NAME__.Domain
{
    public class SampleParentObject : BaseDomainObject<long>
    {
         [MaxLength(100)]
        public string Name { get; set; }
         [MaxLength(100)]
        public string Description { get; set; }
        public virtual IList<SampleChildObject> Children { get; set; }  
    }
}