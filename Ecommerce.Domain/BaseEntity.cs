using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Ecommerce.Domain
{
    public abstract class  BaseEntity
    {
		public virtual DateTime CreatedDateTimeUTC { get; set; }
		public virtual DateTime ModifiedUTC { get; set; }

		[Column(TypeName = "char(32)")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public virtual string ModifiedById { get; set; }
	
		[Column(TypeName = "char(32)")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public virtual string CreatedById { get; set; }



		// this method have to cahnge
		/// <summary>
		/// Use this method to time stamp created object 
		/// </summary>
		public void CreateTimeStamp(User user)
		{
		
			CreatedDateTimeUTC = DateTime.UtcNow;
			CreatedById = user.Id.ToString();
			ModifyTimeStamp(user);
		}


		public void CreateTimeStamp()
		{
			CreatedById = "N/A";
			CreatedDateTimeUTC = DateTime.UtcNow;
			ModifyTimeStamp();
		}
		public void ModifyTimeStamp()
		{
			ModifiedById = "N/A";
			ModifiedUTC = DateTime.UtcNow;
		}

		public void ModifyTimeStamp(User user)
		{
			
			ModifiedUTC = DateTime.UtcNow;
			ModifiedById = user.Id.ToString();

		}

		//public List<string> UpdateUdatebleCopyFileds<TEntity>(TEntity entity) where TEntity : BaseEntity => CheckFields<TEntity>(entity, typeof(UnlockedUponRelation));

		public List<string> UpdateFields<TEntity>(TEntity entity) where TEntity : BaseEntity => CheckFields<TEntity>(entity, typeof(EditableField));

		//public List<string> CopyFields<TEntity>(TEntity entity) where TEntity : BaseEntity => CheckFields<TEntity>(entity, typeof(LockedUponRelation));

		private List<string> CheckFields<TEntity>(TEntity tentity, Type type) where TEntity : BaseEntity
		{
			try
			{
				var changedFields = new List<string>();
				var properties = this.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, type)).ToList();
				foreach (var property in properties)
				{
					var value = property.GetValue(this);
					var sourceValue = property.GetValue(tentity);

					if (!value.IsEquals(sourceValue))
					{
						property.SetValue(this, sourceValue);
						changedFields.Add(property.Name);
					}
				}
				return changedFields;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
	}
}

