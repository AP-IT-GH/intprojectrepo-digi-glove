set arg1=%1
set arg2=%~dp0


cd %arg1%
curl https://bootstrap.pypa.io/get-pip.py -o get-pip.py
python get-pip.py
del get-pip.py
cd Scripts
pip install virtualenv
virtualenv "%arg2%VirtualEnv"
cd "%arg2%VirtualEnv\Scripts\"
activate.bat & pip install -r "%arg2%requirements.txt" & deactivate
