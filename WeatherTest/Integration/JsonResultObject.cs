using System;
using System.Collections.Generic;

namespace WeatherTest.Integration
{
    public class JsonResultObject
    {
        public Properties properties { get; set; }

        public class Properties
        {
            public DateTime generatedAt { get; set; }
            public IEnumerable<Period> periods { get; set; }
        }

        public class Period
        {
            public DateTime startTime { get; set; }
            public DateTime endTime { get; set; }
            public int temperature { get; set; }
            public string temperatureUnit { get; set; }
        }
    }
}
