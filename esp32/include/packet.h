#define SENSOR_VAR_COUNT 13
#define IMU_VAR_COUNT 7
/**
 * @brief Sensor data struct.
*/
typedef struct{
    int64_t capture_time;           // Time of capture
    uint8_t data[SENSOR_VAR_COUNT]; // Sensor data itself in pre-defined order
}sensor_data_t;

/**
 * @brief IMU data struct. 
*/
typedef struct{
    int64_t capture_time;       // Time of capture
    int16_t data[IMU_VAR_COUNT];  // IMU data itself in pre-defined order
}imu_data_t;