using DAL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class BaseMainEntity : BaseEntity
    {
        /// <summary>
        /// This is date and time of creation data.
        /// </summary>
        public DateTime? CreatedDateTime { get; set; }

        /// <summary>
        /// This is userId of who created this data.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// This filed is date and time of updated data, this is null in if data is never updated.
        /// </summary>
        public DateTime? UpdatedDateTime { get; set; }

        /// <summary>
        /// This is userId of who updated data, this is null in if data is never updated.
        /// </summary>
        public string UpdatedBy { get; set; }

        #region all navigation fields

        /// <summary>
        /// CreatedUser object of this entity.
        /// </summary>
        [ForeignKey("CreatedBy")]
        public ApplicationUser UserCreatedBy { get; set; }

        /// <summary>
        /// UpdatedUser object of this entity.
        /// </summary>
        [ForeignKey("UpdatedBy")]
        public ApplicationUser UserUpdatedBy { get; set; }
        #endregion
    }
}
