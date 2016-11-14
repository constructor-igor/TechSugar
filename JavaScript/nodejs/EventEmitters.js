//
// https://nodejs.org/api/events.html
//

const EventEmitter = require('events');
//class MyEmitter extends EventEmitter {}
const myEmitter = new EventEmitter();
myEmitter.on('event1', () => {
  console.log('[event1] an event occurred!');
});
myEmitter.on('event2', function(a, b){
    console.log('[event2]', a, b, this);
});
myEmitter.on('event3', (a, b) => {
    console.log('[event3]', a, b, this);
});
myEmitter.on('event4', (a, b) => {
    setImmediate(()=>{  
        console.log('[event4]', a, b, this);});
});
myEmitter.once('event5', (a, b) => {
    console.log('[event5]', a, b, this);
});

myEmitter.emit('event1');
myEmitter.emit('event2', 1, 2);
myEmitter.emit('event3', 1, 2);
myEmitter.emit('event4', 1, 10);
myEmitter.emit('event4', 2, 20);

myEmitter.emit('event5', 5, 20);
myEmitter.emit('event5', 5, 20);
