import png
import random
import time

width = 1280
height = 720
for i in range(11):
    img = []
    for y in range(height):
        row = ()
        for x in range(width):
            color = random.randint(1,4)
            if (color == 1):
                row = row + (249,245,206)
            elif (color == 2):
                row = row + (227,206,139)
            else:
                row = row + (158,126,68)
        img.append(row)
    if(i<=9):
        with open('Cave0' + str(i) + '.png', 'wb') as f:
            w = png.Writer(width, height, greyscale=False)
            w.write(f, img)
    else:
        with open('Cave' + str(i) + '.png', 'wb') as f:
            w = png.Writer(width, height, greyscale=False)
            w.write(f, img)