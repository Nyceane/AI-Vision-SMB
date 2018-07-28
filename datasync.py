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
        temp = str(file.read().replace('\n',''))
        if peoplestring != temp and temp != '':
            peoplestring = temp
            channel.send(peoplestring.encode())
            print(peoplestring)
        file.close()       
        file = open('bottles.txt', 'r')
        temp = str(file.read().replace('\n',''))
        if bottlestring != temp and temp != '':
            bottlestring = temp
            channel.send(bottlestring.encode())
            print(bottlestring)
        file.close()       
        time.sleep(1)

if __name__ == "__main__":
    main()
