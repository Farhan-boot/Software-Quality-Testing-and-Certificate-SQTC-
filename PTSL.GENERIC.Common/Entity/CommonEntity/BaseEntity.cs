using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
namespace PTSL.GENERIC.Common.Entity.CommonEntity
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", Order = 0, TypeName = "bigint")]
        public long Id { get; set; }

        //[Column("CreatedAt", Order = 1, TypeName = "datetime2 (3)")]
        [Column("CreatedAt", Order = 1, TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; }

        //[Column("UpdatedAt", Order = 2, TypeName = "datetime2 (3)")]
        [Column("UpdatedAt", Order = 2, TypeName = "timestamp")]
        public DateTime? UpdatedAt { get; set; }

        //[Column("DeletedAt", Order = 3, TypeName = "datetime2 (3)")]
        [Column("DeletedAt", Order = 3, TypeName = "timestamp")]
        public DateTime? DeletedAt { get; set; }

        [Column("IsDeleted", Order = 4, TypeName = "boolean")]
        public bool IsDeleted { get; set; }

        [Column("IsActive", Order = 5, TypeName = "boolean")]
        public bool IsActive { get; set; }

        //[Column("IsDeleted", Order = 4, TypeName = "int")]
        //public int IsDeleted { get; set; }

        //[Column("IsActive", Order = 5, TypeName = "int")]
        //public int IsActive { get; set; }

        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public long? DeletedBy { get; set; }

        static BaseEntity() // Must be static for the triggers to be registered only once
        {
            Triggers<BaseEntity>.Inserting += entry =>
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.IsDeleted = false;
                entry.Entity.IsActive = true;
                //entry.Entity.IsDeleted = 0;
                //entry.Entity.IsActive = 1;
            };

            Triggers<BaseEntity>.Updating += entry =>
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                //entry.Entity.ModifiedBy = 1;
            };


            Triggers<BaseEntity>.Deleting += entry =>
            {
                entry.Entity.DeletedAt = DateTime.UtcNow;
                entry.Entity.IsDeleted = true;
                //entry.Entity.IsDeleted = 1;
                entry.Cancel = true;
            };
        }
    }
}
