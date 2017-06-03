namespace DND5ECS.DataAccessLayer
{
    public abstract class XMLFile
    {
        #region Constructors
        /// <summary>
        /// Constructor for XML File object, which takes the path to the file and what type of file it is.
        /// </summary>
        /// <param name="resource">path to the file to be loaded</param>
        protected XMLFile(string resource)
        {
            ResourcePath = resource;
        }
        #endregion

        #region Properties
        private string resourcePath;

        /// <summary>
        /// publically readable path to the document to be loaded
        /// </summary>
        public string ResourcePath
        {
            get { return resourcePath; }
            private set { resourcePath = value; }
        }
        #endregion

        #region Abstract Methods
        public abstract bool ProcessFileTags();
        #endregion
    }
}
