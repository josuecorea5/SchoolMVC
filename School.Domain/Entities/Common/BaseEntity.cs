using School.Domain;
using School.Domain.Enums;

namespace School.Domain.Entities
{
    public abstract class BaseEntity
	{
		public int Id { get; set; }
		public StatusEntity Status { get; set; }
	}
}
