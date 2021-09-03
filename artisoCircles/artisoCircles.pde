
PImage logo;

void setup(){
    size(868, 908, P2D);
    logo = loadImage("innolizer-signet.png");
}

void draw(){
    background(255);
    
    // image(cam, 0, 0, width, height);

    int diameter = int( map(mouseX, 0, width, 5, 50));
    println(diameter);
    logo.loadPixels();
    fill(0);
    stroke(0);
    for (int x = diameter/2; x < width; x+=diameter) {
        for (int y = diameter/2; y < height; y+=diameter) {
            int pixelvalue = logo.pixels[x + y * logo.width];
            float pixelbrightness = brightness(pixelvalue);
            float value = map(pixelbrightness, 190, 40, 0, diameter);
            ellipse(x, y, value, value);
        }
    }
}
