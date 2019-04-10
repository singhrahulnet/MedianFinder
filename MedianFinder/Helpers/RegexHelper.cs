using System.Collections.Generic;

namespace MedianFinder.Helpers
{
    public static class RegexHelper
    {
        public static string FileInitialPattern { get => @"(?<fileType>([a-zA-Z]+))[_]"; }
        public static string FileTypeGroupName { get => "fileType"; }

        public static string FileTypesRegexPattern(Dictionary<string, string> fileTypes)
        {
            string typeExp = string.Empty;
            //Building regex for matching correct file names. The output would be something like "(.+\\(?=(LP|TOU)))"
            foreach (var type in fileTypes)
            {
                typeExp += typeExp + type.Key + "|";
            }
            //delete the last pipe
            typeExp = typeExp.Remove(typeExp.Length - 1, 1);

            return $@"(.+\\(?=({typeExp})))";
        }
    }
}
