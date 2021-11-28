import uuid
import json


class SimulationDatabase:

    def __init__(self):
        self.data = {
            'cars': []
        }

    def _find(self, obj_key):
        car_list = self.data['cars']
        for i in range(len(car_list)):
            if car_list[i]['id'] == obj_key:
                return i
        raise ValueError

    def add_car(self, x=0, y=0, z=0, car_id=None):
        if car_id is None:
            car_id = str(uuid.uuid4())

        new_car = {
            'id': car_id,
            'x': x,
            'y': y,
            'z': z
        }

        self.data['cars'].append(new_car)
        return car_id

    def update_car(self, car_id, x=0, y=0, z=0):
        try:
            ind = self._find(car_id)
            car_list = self.data['cars']
            car_list[ind]['x'] = x
            car_list[ind]['y'] = y
            car_list[ind]['z'] = z
            return True
        except ValueError:
            print("Car not found")
            return False

    def delete_car(self, car_id):
        try:
            ind = self._find(car_id)
            car_list = self.data['cars']
            del car_list[ind]
            return True
        except ValueError:
            print("Object not found")
            return False

    def string(self):
        return json.dumps(self.data)

    def print(self):
        print(self.data)
