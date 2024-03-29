import cv2
from cvzone.HandTrackingModule import HandDetector
import socket

# Parameters
width, height = 1280, 720

#webcam

cap = cv2.VideoCapture(0)
cap.set(3, width)
cap.set(4, height)

# Communication
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 5052)


# Hand Detector
detector = HandDetector(maxHands=1, detectionCon=0.8)
while True:
        success, img = cap.read()

        hands, img = detector.findHands(img)
        data = []
        if hands:
            hand = hands[0]
            landmarks = hand['lmList']


            for lm in landmarks:
                data.extend([lm[0], height-lm[1], lm[2]])
            sock.sendto(str.encode(str(data)), serverAddressPort)

        img = cv2.resize(img,(0,0),None,0.5,0.5)
        cv2.imshow("Image",img)
        cv2.waitKey(1)

