import requests
import time
r = requests.post('http://localhost:8080/api/register', json={"username": "admin", "pwd": "testpwd"})
time.sleep(1)
r = requests.post('http://localhost:8080/api/login', json={"username": "admin", "pwd": "testpwd"})
time.sleep(1)
print(r.status_code)
print(r.json())
r = requests.post('http://localhost:8080/api/login', json={"username": "admin", "pwd": "wrongpwd"})
print(r.status_code)
print(r.json())
