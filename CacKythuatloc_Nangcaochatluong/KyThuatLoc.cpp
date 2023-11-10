#include <opencv2/opencv.hpp>

using namespace cv;

int main() {
    Mat image = imread("123.png");
    // Áp dụng lọc Gauss
    Mat blurred;
    GaussianBlur(image, blurred, Size(5, 5), 0);
    // Áp dụng Median Blur
    Mat blurred;
    medianBlur(image, blurred, 5);
    // Áp dụng lọc song phương
    Mat filtered;
    bilateralFilter(image, filtered, 15, 75, 75);
    // Convert to grayscale
    Mat gray;
    cvtColor(image, gray, COLOR_BGR2GRAY);
    // Apply Laplacian Filter
    Mat filtered; 
    Laplacian(gray, filtered, CV_16S); 
    // Convert back to 8-bit image
    Mat absFiltered; 
    convertScaleAbs(filtered, absFiltered); 
    // Apply Sobel Filter
    Mat gradX, gradY; 
    Sobel(gray, gradX, CV_16S, 1, 0); 
    Sobel(gray, gradY, CV_16S, 0, 1);
    // Convert back to 8-bit image 
    Mat absGradX, absGradY; 
    convertScaleAbs(gradX, absGradX); 
    convertScaleAbs(gradY, absGradY);

    // Combine X and Y gradients
    Mat combined;
    addWeighted(absGradX, 0.5, absGradY, 0.5, 0, combined);

    // Apply Canny Edge Detection
    Mat edges;
    Canny(gray, edges, 100, 200);

    // Hiển thị kết quả
    imshow("Median  Image", blurred);
    imshow("Gauss Blur Image ", blurred);
    imshow("Filtered Image ", filtered);
    imshow("Laplacian Image", absFiltered);
    imshow("Sobel Image", combined);
    imshow("Edges", edges);
    waitKey(0);
    return 0;
}