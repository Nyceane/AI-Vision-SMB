import time
from helium_client import Helium
helium = Helium(b'/dev/ttyS5')
helium.connect()
channel = helium.create_channel(b"Azure IoT App")

def main():
    peoplestring = ""
    bottlestring = ""
    while True:
        file = open('people.txt', 'r')
        if peoplestring != file.read().replace('\n',''):        
            # Sending in information for people that it's detecting
            channel.send(datastring.encode())
        file.close()
        file = open('bottle.txt', 'r')
        if bottlestring != file.read().replace('\n',''):        
            channel.send(datastring.encode())
        file.close()
        time.sleep(1)

if __name__ == "__main__":
    main()
