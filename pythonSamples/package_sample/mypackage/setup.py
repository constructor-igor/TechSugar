import setuptools
setuptools.setup(
    name="mypackage",
    version="0.1.0",
    url="https://github.com/constructor-igor/TechSugar/",
    author="Igor (constructor)",
    author_email="email@gmail.com",
    description="Demo for package building",
	long_description=open('README.rst').read(),
    packages=setuptools.find_packages(),
    install_requires=[],	
    classifiers=[
        'Development Status :: 2 - Pre-Alpha',
        'Programming Language :: Python',
        'Programming Language :: Python :: 3',
        'Programming Language :: Python :: 3.6',
    ],	
)