using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BOL
{
    [Serializable]
    public class Category
    {
        #region propriété de la classe
        private int _id;
        private string _name;
        private string _description;
        private int _categoryTemplateId;
        private string _metaKeywords;
        private string _metaDescription;
        private string _metaTitle;
        private int _parentCategoryId;
        private int _pictureId;
        private int _pageSize;
        private bool _allowCustomersToSelectPageSize;
        private string _pageSizeOptions;
        private string _priceRanges;
        private bool _showOnHomePage;
        private bool _includeInTopMenu;
        private bool _hasDiscountsApplied;
        private bool _subjectToAcl;
        private bool _limitedToStores;
        private bool _published;
        private bool _deleted;
        private int _displayOrder;
        private DateTime _createdOnUtc;
        private DateTime _updatedOnUtc;
        private Picture _picture;
        #endregion

        public Category ()
        {
            this.Products = new HashSet<Product>();
            this.Categories = new HashSet<Category>();
        }

        #region encapsulation
        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int CategoryTemplateId { get => _categoryTemplateId; set => _categoryTemplateId = value; }
        public string MetaKeywords { get => _metaKeywords; set => _metaKeywords = value; }
        public string MetaDescription { get => _metaDescription; set => _metaDescription = value; }
        public string MetaTitle { get => _metaTitle; set => _metaTitle = value; }
        public int ParentCategoryId { get => _parentCategoryId; set => _parentCategoryId = value; }
        public int PictureId { get => _pictureId; set => _pictureId = value; }
        public int PageSize { get => _pageSize; set => _pageSize = value; }
        public bool AllowCustomersToSelectPageSize { get => _allowCustomersToSelectPageSize; set => _allowCustomersToSelectPageSize = value; }
        public string PageSizeOptions { get => _pageSizeOptions; set => _pageSizeOptions = value; }
        public string PriceRanges { get => _priceRanges; set => _priceRanges = value; }
        public bool ShowOnHomePage { get => _showOnHomePage; set => _showOnHomePage = value; }
        public bool IncludeInTopMenu { get => _includeInTopMenu; set => _includeInTopMenu = value; }
        public bool HasDiscountsApplied { get => _hasDiscountsApplied; set => _hasDiscountsApplied = value; }
        public bool SubjectToAcl { get => _subjectToAcl; set => _subjectToAcl = value; }
        public bool LimitedToStores { get => _limitedToStores; set => _limitedToStores = value; }
        public bool Published { get => _published; set => _published = value; }
        public bool Deleted { get => _deleted; set => _deleted = value; }
        public int DisplayOrder { get => _displayOrder; set => _displayOrder = value; }
        public DateTime CreatedOnUtc { get => _createdOnUtc; set => _createdOnUtc = value; }
        public DateTime UpdatedOnUtc { get => _updatedOnUtc; set => _updatedOnUtc = value; }
        public Picture Picture { get => _picture; set => _picture = value; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        #endregion

    }
}
