using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaLand.Planets
{
    static class Constants
    {
        public const float EARTH_SOLAR_SPEED = 0.00001f; //Determine how fast the solar system rotates.

        public const float PLANET_SIZE_RATIO = 1.0f; //Defines how much bigger the planets are.
        public const float PLANET_DISTANCE_RATIO = 0.001f;
        public const float SUN_SIZE_RATIO = PLANET_DISTANCE_RATIO;
        public const float SUN_SIZE = 1300000f * SUN_SIZE_RATIO;

        public const float MARS_RELATIVE_SIZE = 0.151f * PLANET_SIZE_RATIO; //Defines how big mars is.
        public const float MERCURY_RELATIVE_SIZE = 0.056f * PLANET_SIZE_RATIO; //Defines how big mercury is.
        public const float VENUS_RELATIVE_SIZE = 0.866f * PLANET_SIZE_RATIO; //Defines how big venus is.
        public const float MOON_RELATIVE_SIZE = 0.02f * PLANET_SIZE_RATIO; //Defines how big the moon is.
        public const float JUPITER_RELATIVE_SIZE = 1321.3f * PLANET_SIZE_RATIO; //Defines how big jupiter is.
        public const float SATURN_RELATIVE_SIZE = 763.59f * PLANET_SIZE_RATIO; //Defines how big saturn is.
        public const float URANUS_RELATIVE_SIZE = 63.086f * PLANET_SIZE_RATIO; //Defines how big uranus is.
        public const float NEPTUNE_RELATIVE_SIZE = 57.74f * PLANET_SIZE_RATIO; //Defines how big neptune is.

        public const float EARTH_DISTANCE_FROM_SUN = 149669180f * PLANET_DISTANCE_RATIO;
        public const float MERCURY_DISTANCE_FROM_SUN = 46001200f * PLANET_DISTANCE_RATIO;
        public const float VENUS_DISTANCE_FROM_SUN = 107477000f * PLANET_DISTANCE_RATIO;
        public const float MARS_DISTANCE_FROM_SUN = 206669000f * PLANET_DISTANCE_RATIO;
        public const float MOON_DISTANCE_FROM_EARTH = 405410f * PLANET_DISTANCE_RATIO;
        public const float ASTEROID_BELT_DISTANCE_FROM_SUN = 404000000f * PLANET_DISTANCE_RATIO;
        public const float JUPITER_DISTANCE_FROM_SUN = 816520800f * PLANET_DISTANCE_RATIO;
        public const float SATURN_DISTANCE_FROM_SUN = 1426666422f * PLANET_DISTANCE_RATIO;
        public const float URANUS_DISTANCE_FROM_SUN = 2870658186f * PLANET_DISTANCE_RATIO;
        public const float NEPTUNE_DISTANCE_FROM_SUN = 4498396441f * PLANET_DISTANCE_RATIO;

        public const float MERCURY_NUMBER_OF_EARTH_DAYS_TO_ORBIT = 88f;
        public const float VENUS_NUMBER_OF_EARTH_DAYS_TO_ORBIT = 224.7f;
        public const float EARTH_NUMBER_OF_EARTH_DAYS_TO_ORBIT = 365.25f;
        public const float MOON_NUMBER_OF_EARTH_DAYS_TO_ORBIT = 365.25f / 12f;
        public const float MARS_NUMBER_OF_EARTH_DAYS_TO_ORBIT = 686.971f;
        public const float JUPITER_NUMBER_OF_EARTH_DAYS_TO_ORBIT = 4331.572f; //LOL
        public const float SATURN_NUMBER_OF_EARTH_DAYS_TO_ORBIT = 10759.22f;
        public const float URANUS_NUMBER_OF_EARTH_DAYS_TO_ORBIT = 30799.095f;
        public const float NEPTUNE_NUMBER_OF_EARTH_DAYS_TO_ORBIT = 60190f;


        public const float MARS_NUMBER_OF_SPINS_PER_ORBIT = 686.971f * 0.97f;
        public const float EARTH_NUMBER_OF_SPINS_PER_ORBIT = 365.25f;
        public const float MERCURY_NUMBER_OF_SPINS_PER_ORBIT = 9f / 6f;
        public const float VENUS_NUMBER_OF_SPINS_PER_ORBIT = -1.5f;
        public const float MOON_NUMBER_OF_SPINS_PER_ORBIT = 1f;
        public const float JUPITER_NUMBER_OF_SPINS_PER_ORBIT = 4331.572f / 398.88f;
        public const float SATURN_NUMBER_OF_SPINS_PER_ORBIT = 10759.22f / 378.09f;
        public const float URANUS_NUMBER_OF_SPINS_PER_ORBIT = 30799.095f / 369.66f;
        public const float NEPTUNE_NUMBER_OF_SPINS_PER_ORBIT = 60190f / 367.49f;
    }
}
