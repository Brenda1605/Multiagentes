import json

import requests as req


class Client:
    def __init__(self, url=None):
        if url is None:
            self.url = "http://127.0.0.1:8000"
        else:
            self.url = url

    def add_car(self, id, pos):
        data = {
            'action': 'add',
            'id': id,
            'x': pos[1],
            'y': pos[0],
            'z': 0
        }
        r = req.post(self.url, data=json.dumps(data))
        r_obj = r.json()
        print(r_obj['message'])

        return r.status_code

    def update_car(self, id, pos):
        data = {
            'action': 'update',
            'id': id,
            'x': pos[1],
            'y': pos[0],
            'z': 0
        }

        r = req.post(self.url, data=json.dumps(data))
        print(r.text)

        return r.status_code

    def delete_car(self, id):
        data = {
            'action': 'delete',
            'id': id
        }

        r = req.post(self.url, data=json.dumps(data))
        print(r.text)

        return r.status_code


if __name__ == "__main__":
    remote_server = input("¿Desea conectar con un servidor remoto?  [y]yes / [n]no: ")
    url = None
    if remote_server == 'y':
        url = input("Dir. IP: ")

    cl = Client(url)
    some_id = "abc"
    cl.add_car(some_id, (1, 1))
    cl.delete_car(some_id)

