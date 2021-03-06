﻿namespace SolutionStartPage.Shared.Models
{
    using System.Linq;
    using System.Xml.Serialization;
    using Views.SolutionPageView;

    [XmlRoot]
    public class SolutionPageConfiguration
    {
        /////////////////////////////////////////////////////////

        #region Fields

        private int _columns;

        #endregion

        /////////////////////////////////////////////////////////

        #region Properties

        [XmlElement]
        public int Columns
        {
            get { return _columns; }
            set
            {
                if (value == _columns) return;
                if (value < 1)
                    _columns = 1;
                else if (value > 3)
                    _columns = 3;
                else
                    _columns = value;
            }
        }

        [XmlElement]
        public bool DisplayFolders { get; set; }

        [XmlArray("SolutionGroups")]
        [XmlArrayItem("SolutionGroup")]
        public SolutionGroup[] SolutionGroups { get; set; }

        #endregion

        /////////////////////////////////////////////////////////

        #region Constructors

        public SolutionPageConfiguration()
        {
            Columns = 3;
            DisplayFolders = true;
            SolutionGroups = new SolutionGroup[0];
        }

        #endregion

        /////////////////////////////////////////////////////////

        #region Public Methods

        public SolutionPageConfiguration ApplyViewModel(ISolutionPageViewModel vm)
        {
            Columns = vm.Columns;
            DisplayFolders = vm.DisplayFolders;
            SolutionGroups = vm.SolutionGroups.ToArray();

            return this;
        }

        #endregion
    }
}