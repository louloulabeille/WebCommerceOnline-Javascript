using System;
using System.Collections.Generic;
using System.Text;

namespace BOL
{
    public class Picture
    {
        #region proriétés de la classe
        private int _id;
        private byte[] _pictureBinary;
        private string _mimeType;
        private string _seoFilename;
        private string _altAttribute;
        private string _titleAttribute;
        private bool _isNew;
        #endregion

        #region encapsulation
        public int Id { get => _id; set => _id = value; }
        public byte[] PictureBinary { get => _pictureBinary; set => _pictureBinary = value; }
        public string MimeType { get => _mimeType; set => _mimeType = value; }
        public string SeoFilename { get => _seoFilename; set => _seoFilename = value; }
        public string AltAttribute { get => _altAttribute; set => _altAttribute = value; }
        public string TitleAttribute { get => _titleAttribute; set => _titleAttribute = value; }
        public bool IsNew { get => _isNew; set => _isNew = value; }
        #endregion

    }
}
