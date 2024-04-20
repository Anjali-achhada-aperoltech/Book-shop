using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    
        public static class StringHelpers
        {
            public static string Truncate(string text, int maxLength)
            {
                if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                {
                    return text;
                }

                // You can modify the way it truncates the text, for example, to cut at full words
                return text.Substring(0, maxLength) + "...";
            }
        }

    }

