using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaLand.Planets
{
    static class Constants
    {
        public const float EARTH_SOLAR_SPEED = 0.00001f; //Determine how fast the solar system rotates.

        public const float PLANET_SIZE_RATIO = 10.0f; //Defines how much bigger the planets are.
        public const float PLANET_DISTANCE_RATIO = 0.00015f;
        public const float SUN_SIZE_RATIO = PLANET_DISTANCE_RATIO;
        public const float SUN_SIZE = 1300000f * SUN_SIZE_RATIO;

        public const float MARS_RELATIVE_SIZE = 0.151f * PLANET_SIZE_RATIO; //Defines how big mars is.
        public const float MERCURY_RELATIVE_SIZE = 0.056f * PLANET_SIZE_RATIO; //Defines how big mercury is.
        public const float VENUS_RELATIVE_SIZE = 0.866f * PLANET_SIZE_RATIO; //Defines how big venus is.
        public const float MOON_RELATIVE_SIZE = 0.02f * PLANET_SIZE_RATIO; //Defines how big moon is.

        public const float EARTH_DISTANCE_FROM_SUN = 149669180f * PLANET_DISTANCE_RATIO;
        public const float MERCURY_DISTANCE_FROM_SUN = 46001200f * PLANET_DISTANCE_RATIO;
        public const float VENUS_DISTANCE_FROM_SUN = 107477000f * PLANET_DISTANCE_RATIO;
        public const float MARS_DISTANCE_FROM_SUN = 206669000f * PLANET_DISTANCE_RATIO;
        public const float MOON_DISTANCE_FROM_EARTH = 405410f * PLANET_DISTANCE_RATIO;

        public const float MERCURY_NUMBER_OF_EARTH_DAYS_TO_ORBIT = 88f;
        public const float VENUS_NUMBER_OF_EARTH_DAYS_TO_ORBIT = 224.7f;
        public const float EARTH_NUMBER_OF_EARTH_DAYS_TO_ORBIT = 365.25f;
        public const float MOON_NUMBER_OF_EARTH_DAYS_TO_ORBIT = 365.25f / 12f;
        public const float MARS_NUMBER_OF_EARTH_DAYS_TO_ORBIT = 686.971f;

        public const float MARS_NUMBER_OF_SPINS_PER_ORBIT = 686.971f * 0.97f;
        public const float EARTH_NUMBER_OF_SPINS_PER_ORBIT = 365.25f;
        public const float MERCURY_NUMBER_OF_SPINS_PER_ORBIT = 9f / 6f;
        public const float VENUS_NUMBER_OF_SPINS_PER_ORBIT = -1.5f;
        public const float MOON_NUMBER_OF_SPINS_PER_ORBIT = 1f;

    }
}
