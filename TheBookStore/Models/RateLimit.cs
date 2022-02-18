using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace TheBookStore.Models
{
    public class RateLimit
    {
        #region Properties

        private int timeout;
        public int Limit { get; set; }
        public int Hits { get; set; }
        public int RemainingHits
        {
            get
            {
                if (Hits > Limit)
                {
                    return 0;
                }

                return Limit - Hits;
            }
        }

        public string Token { get; set; }
        public DateTime Reset { get; set; }

        public Decimal RemainingSeconds
        {
            get
            {
                return Decimal.Truncate(Convert.ToDecimal((Reset - DateTime.Now).TotalSeconds));
            }
        }
        public bool CanRequest
        {
            get
            {
                if (RemainingSeconds <= 0)
                {
                    RefreshCache();
                }

                return Limit - Hits >= 0;
            }
        }

        #endregion


        public RateLimit(string token, int limit, int timeout)
        {
            this.timeout = timeout;
            Limit = limit;
            Token = token;
            RefreshCache();
        }

        private void RefreshCache()
        {
            Hits = 0;
            Reset = DateTime.Now.AddSeconds(timeout);
            HttpRuntime.Cache.Remove(Token);
            HttpRuntime.Cache.Add(Token, this, null, DateTime.Now.AddSeconds(this.timeout), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        public bool CheckLimit()
        {
            if (Reset < DateTime.Now)
            {
                RefreshCache();
            }

            Hits++;
            HttpRuntime.Cache[Token] = this;
            return CanRequest;
        }


    }
}