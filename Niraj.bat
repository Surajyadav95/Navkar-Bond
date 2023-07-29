git config --global user.name "Surajyadav95"
git config --global user.email "suraj@digidisruptors.com" 
git init
git add .
git commit -m "Initial commit"
git remote add origin https://github.com/Surajyadav95/Navkar-Bond.git
git branch -M main
git push -u origin main
git fetch --all
git checkout -b "dev/Navkar-Bond29_07_2023" 
git push origin dev/Navkar-Bond29_07_2023