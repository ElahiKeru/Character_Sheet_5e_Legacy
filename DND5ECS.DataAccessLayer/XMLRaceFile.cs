using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using DND5ECS.BusinessLogicLayer;

namespace DND5ECS.DataAccessLayer
{
    public class XMLRaceFile : XMLFile
    {
        #region Consctructors
        /// <summary>
        /// public constructor for XMLRaceFile
        /// </summary>
        /// <param name="resource">full path to xml resource file to be loaded</param>
        public XMLRaceFile(string resource) : base(resource)
        {
            racesDocument = XDocument.Load(ResourcePath);
        }
        #endregion

        #region Properties
        private XDocument racesDocument;

        public XDocument RacesDocument
        {
            get { return racesDocument; }
            //private set { racesDocument = value; }
        }

        private List<string> races;

        public List<string> Races
        {
            get
            {
                if(races == null)
                {
                    races = new List<string>();
                }

                return races;
            }
            //private set { races = value; }
        }

        private List<string> raceNames;

        public List<string> RaceNames
        {
            get
            {
                if(raceNames == null)
                {
                    raceNames = new List<string>();
                }

                return raceNames;
            }
            //private set { raceNames = value; }
        }

        private Dictionary<string, Race> raceList;

        public Dictionary<string, Race> RaceList
        {
            get
            { 
                if(raceList == null)
                {
                    raceList = new Dictionary<string, Race>();
                }

                return raceList;
            }
            //private set { raceList = value; }
        }

        private StringBuilder traitSB;

        public StringBuilder TraitSB
        {
            get
            {
                if(traitSB == null)
                {
                    traitSB = new StringBuilder();
                }

                return traitSB;
            }
            set { traitSB = value; }
        }

        #endregion

        #region Methods
        public override bool ProcessFileTags()
        {
            foreach (XElement element in RacesDocument.Descendants().Descendants())
            {
                if (element.Name.ToString().Equals("race"))
                {
                    foreach (XAttribute raceAttribute in element.Attributes())
                    {
                        Races.Add(raceAttribute.Value);
                    }
                    foreach (XAttribute traitAttribute in element.Descendants("trait").Attributes())
                    {
                        if (traitAttribute.Name.ToString().Equals("name"))
                        {
                            TraitSB.AppendFormat("{0}: ", traitAttribute.Value);
                        }
                        else
                        {
                            TraitSB.AppendFormat("{0}\r\n\r\n", traitAttribute.Value);
                        }
                    }

                    RaceNames.Add(Races[0]);
                    RaceList.Add(Races[0], new Race(Races[0], Races[1], Races[2],
                        Races[3], Races[4], Races[5], Races[6], TraitSB.ToString()));

                    Races.Clear();
                    TraitSB.Clear();
                }
            }

            return true;
        }
        #endregion


    }
}
