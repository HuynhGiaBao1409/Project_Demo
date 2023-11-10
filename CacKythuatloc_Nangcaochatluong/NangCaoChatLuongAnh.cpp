#include <opencv2/opencv.hpp>
#include <iostream>

using namespace cv;
using namespace std;

int main()
{
    // Load input image
    Mat input_image = imread("123.png");

    // Convert input image to grayscale
    Mat gray_image;
    cvtColor(input_image, gray_image, COLOR_BGR2GRAY);

    // Apply the Adaptive Bilateral Filter
    Mat filtered_image;
    int diameter = 9; // Diameter of each pixel neighborhood used during filtering
    double sigma_color = 75; // Filter sigma in the color space
    double sigma_space = 75; // Filter sigma in the coordinate space
    bilateralFilter(gray_image, filtered_image, diameter, sigma_color, sigma_space);

    // Convert the output image to 8-bit unsigned integer format
    filtered_image.convertTo(filtered_image, CV_8U);

    // Display the input and output images
    imshow("Input Image", input_image);
    imshow("Output Image", filtered_image);
    waitKey();

    return 0;
}