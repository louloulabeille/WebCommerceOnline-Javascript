using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BOL
{
    [Serializable]
    public class Product
    {
        #region propriété de la classe
        private int _id;
        private int _productTypeId;
        private int _parentGroupedProductId;
        private bool _visibleIndividually;
        private string _name;
        private string _shortDescription;
        private string _fullDescription;
        private string _adminComment;
        private int _productTemplateId;
        private bool _showOnHomePage;
        private string _metaKeywords;
        private string _metaDescription;
        private string _metaTitle;
        private bool _isRental;
        private int _rentalPriceLength;
        private int _rentalPricePeriodId;
        private bool _isTaxExempt;
        private int _taxCategoryId;
        private int _stockQuantity;
        private bool _displayStockAvailability;
        private bool _displayStockQuantity;
        private int _minStockQuantity;
        private int _orderMinimumQuantity;
        private int _orderMaximumQuantity;
        private bool _disableBuyButton;
        private bool _disableWishlistButton;
        private decimal _price;
        private decimal _oldPrice;
        private decimal _specialPrice;
        private DateTime _specialPriceStartDateTimeUtc;
        private DateTime _specialPriceEndDateTimeUtc;
        private decimal _weight;
        private decimal _length;
        private decimal _width;
        private decimal _height;
        private DateTime _availableStartDateTimeUtc;
        private DateTime _availableEndDateTimeUtc;
        private int _displayOrder;
        private bool _published;
        private bool _deleted;
        private DateTime _createdOnUtc;
        private DateTime _updatedOnUtc;
        private Category _category;
        private int _idCategory;
        #endregion

        #region constructeur
        /// <summary>
        /// constructeur de la classe avec intialisation de categories et pictures
        /// </summary>
        public Product()
        {
            this.Pictures = new HashSet<Picture>();
            this.Products = new HashSet<Product>();
        }
        #endregion

        #region encapsulation
        public int Id { get => _id; set => _id = value; }
        public int ProductTypeId { get => _productTypeId; set => _productTypeId = value; }
        public int ParentGroupedProductId { get => _parentGroupedProductId; set => _parentGroupedProductId = value; }
        public bool VisibleIndividually { get => _visibleIndividually; set => _visibleIndividually = value; }
        public string Name { get => _name; set => _name = value; }
        public string ShortDescription { get => _shortDescription; set => _shortDescription = value; }
        public string FullDescription { get => _fullDescription; set => _fullDescription = value; }
        public string AdminComment { get => _adminComment; set => _adminComment = value; }
        public int ProductTemplateId { get => _productTemplateId; set => _productTemplateId = value; }
        public bool ShowOnHomePage { get => _showOnHomePage; set => _showOnHomePage = value; }
        public string MetaKeywords { get => _metaKeywords; set => _metaKeywords = value; }
        public string MetaDescription { get => _metaDescription; set => _metaDescription = value; }
        public string MetaTitle { get => _metaTitle; set => _metaTitle = value; }
        public bool IsRental { get => _isRental; set => _isRental = value; }
        public int RentalPriceLength { get => _rentalPriceLength; set => _rentalPriceLength = value; }
        public int RentalPricePeriodId { get => _rentalPricePeriodId; set => _rentalPricePeriodId = value; }
        public bool IsTaxExempt { get => _isTaxExempt; set => _isTaxExempt = value; }
        public int TaxCategoryId { get => _taxCategoryId; set => _taxCategoryId = value; }
        public int StockQuantity { get => _stockQuantity; set => _stockQuantity = value; }
        public bool DisplayStockAvailability { get => _displayStockAvailability; set => _displayStockAvailability = value; }
        public bool DisplayStockQuantity { get => _displayStockQuantity; set => _displayStockQuantity = value; }
        public int MinStockQuantity { get => _minStockQuantity; set => _minStockQuantity = value; }
        public int OrderMinimumQuantity { get => _orderMinimumQuantity; set => _orderMinimumQuantity = value; }
        public int OrderMaximumQuantity { get => _orderMaximumQuantity; set => _orderMaximumQuantity = value; }
        public bool DisableBuyButton { get => _disableBuyButton; set => _disableBuyButton = value; }
        public bool DisableWishlistButton { get => _disableWishlistButton; set => _disableWishlistButton = value; }
        public decimal Price { get => _price; set => _price = value; }
        public decimal OldPrice { get => _oldPrice; set => _oldPrice = value; }
        public decimal SpecialPrice { get => _specialPrice; set => _specialPrice = value; }
        public DateTime SpecialPriceStartDateTimeUtc { get => _specialPriceStartDateTimeUtc; set => _specialPriceStartDateTimeUtc = value; }
        public DateTime SpecialPriceEndDateTimeUtc { get => _specialPriceEndDateTimeUtc; set => _specialPriceEndDateTimeUtc = value; }
        public decimal Weight { get => _weight; set => _weight = value; }
        public decimal Length { get => _length; set => _length = value; }
        public decimal Width { get => _width; set => _width = value; }
        public decimal Height { get => _height; set => _height = value; }
        public DateTime AvailableStartDateTimeUtc { get => _availableStartDateTimeUtc; set => _availableStartDateTimeUtc = value; }
        public DateTime AvailableEndDateTimeUtc { get => _availableEndDateTimeUtc; set => _availableEndDateTimeUtc = value; }
        public int DisplayOrder { get => _displayOrder; set => _displayOrder = value; }
        public bool Published { get => _published; set => _published = value; }
        public bool Deleted { get => _deleted; set => _deleted = value; }
        public DateTime CreatedOnUtc { get => _createdOnUtc; set => _createdOnUtc = value; }
        public DateTime UpdatedOnUtc { get => _updatedOnUtc; set => _updatedOnUtc = value; }
        public virtual ICollection<Picture> Pictures { get; set; }      // -- un produit peut avoir une collection object

        public virtual ICollection<Product> Products { get; set; }      // -- un produit peut avoir des sous produits
        #endregion
    }
}
