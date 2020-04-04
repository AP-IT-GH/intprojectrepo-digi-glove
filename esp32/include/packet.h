#define SENSOR_DATA_SIZE 13
#define IMU_DATA_SIZE 7
/**
 * @brief Sensor data struct.
*/
typedef struct{
    int64_t capture_time;           // Time of capture
    uint8_t data[SENSOR_DATA_SIZE]; // Sensor data itself in pre-defined order
}sensor_data_t;

/**
 * @brief IMU data struct. 
*/
typedef struct{
    int64_t capture_time;       // Time of capture
    float data[IMU_DATA_SIZE];  // IMU data itself in pre-defined order
}imu_data_t;