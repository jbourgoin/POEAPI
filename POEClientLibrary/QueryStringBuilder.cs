using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POEClientLibrary
{
    class QueryStringBuilder
    {
        private string route;
        private Dictionary<string, string> KeyValuePairs = new Dictionary<string,string>();

        public QueryStringBuilder(string route)
        {
            this.route = route;
        }

        public QueryStringBuilder Add(string key, string value)
        {
            if (value != null)
                KeyValuePairs[key] = value;

            return this;
        }

        public string Builder()
        {
            string querystring = string.Join("&", KeyValuePairs.Select(a => a.Key + "=" + a.Value));
            return this.route+"?"+ querystring;
        }

    }
}
