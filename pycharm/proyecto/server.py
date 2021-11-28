# Server
import json
from http.server import HTTPServer, BaseHTTPRequestHandler
import logging

# Database
import database

db = database.SimulationDatabase()


class MyHandler(BaseHTTPRequestHandler):
    def _set_response(self):
        self.send_response(200)
        self.send_header('Content-type', 'text/html')
        self.send_header('Access-Control-Allow-Origin', '*')
        self.end_headers()

    def do_GET(self):
        logging.info("GET request,\nPath: %s\nHeaders:\n%s\n", str(self.path), str(self.headers))
        self._set_response()
        self.wfile.write(bytes(db.string(), 'utf-8'))

    def do_POST(self):
        content_length = int(self.headers['Content-Length'])  # size of data
        post_data = self.rfile.read(content_length).decode('utf-8')
        logging.info("POST request,\nPath: %s\nHeaders:\n%s\n\nBody:\n%s\n",
                     str(self.path), str(self.headers), post_data)

        # analyze post action type
        post_data_json = json.loads(post_data)
        action = post_data_json['action']
        self._set_response()
        if action == 'add':
            obj_id = db.add_car(post_data_json['x'], post_data_json['y'], post_data_json['z'], post_data_json['id'])
            res = {'id': obj_id, 'message': 'New car added, id: ' + str(obj_id)}
            self.wfile.write(bytes(json.dumps(res), 'utf-8'))
        elif action == 'update':
            db.update_car(post_data_json['id'], post_data_json['x'], post_data_json['y'], post_data_json['z'])
        elif action == 'delete':
            db.delete_car(post_data_json['id'])


def run():
    socket = ('localhost', 8000)
    httpd = HTTPServer(socket, MyHandler)
    httpd.serve_forever()


if __name__ == "__main__":
    run()
