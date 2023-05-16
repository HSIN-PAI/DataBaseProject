namespace ClinicServiceSystem.Models
{
    public abstract class ViewModel
    {
        #region CRUDModel Setting
        /// <summary>
        /// In Controller's action code
        /// </summary>
        public readonly string Create = "CREATE";
        public readonly string Read = "READ";
        public readonly string Update = "UPDATE";
        public readonly string Delete = "DELETE";

        /// <summary>
        /// In View's operate code
        /// </summary>
        public class ViewOperCode
        {
            public const string Create = "add";
            public const string Read = "detail";
            public const string Update = "edit";
            public const string Delete = "delete";
        }

        /// <summary>
        /// In View's title name
        /// </summary>
        public class ViewShowCode
        {
            public const string Create = "Add";
            public const string Read = "Detail";
            public const string Update = "Edit";
            public const string Delete = "Delete";
        }
        #endregion

        #region CRUDModel Property
        public string OperCode { get; set; }

        public string ActionCode
        {
            get { return GetActionCode(); }
        }

        public string ShowCode
        {
            get { return GetShowCode(); }
        }
        #endregion

        #region CRUDModel Method
        private string GetActionCode()
        {
            string result = "";

            switch (OperCode)
            {
                case ViewOperCode.Create:
                    result = Create;
                    break;

                case ViewOperCode.Read:
                    result = Read;
                    break;

                case ViewOperCode.Update:
                    result = Update;
                    break;

                case ViewOperCode.Delete:
                    result = Delete;
                    break;

                default:
                    break;
            }

            return result;
        }

        private string GetShowCode()
        {
            string result = "";

            switch (OperCode)
            {
                case ViewOperCode.Create:
                    result = ViewShowCode.Create;
                    break;

                case ViewOperCode.Read:
                    result = ViewShowCode.Read;
                    break;

                case ViewOperCode.Update:
                    result = ViewShowCode.Update;
                    break;

                case ViewOperCode.Delete:
                    result = ViewShowCode.Delete;
                    break;

                default:
                    break;
            }

            return result;
        }
        #endregion
    }
}