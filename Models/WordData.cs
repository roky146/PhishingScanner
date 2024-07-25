using System.Collections.Generic;

namespace PhishingScanner.Models
{
    public class WordData
    {
        public string Word { get; set; }
        public int OriginalWeight { get; set; }
        public int Occurrences { get; set; }
        public int NewWeight { get; set; }
    }
}
