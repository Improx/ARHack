const express = require('express')
const Message = require('./models/Message')
const mongoose = require('mongoose')
const connect = require('connect')
const bodyParser = require('body-parser')
const database = 'mongodb://localhost:27017'

const app = express()
const port = 3000

mongoose.connect(database)

app.use(bodyParser.urlencoded({extended: false}));

app.get('/', (req, res) => res.send('Hello World!'))

app.get('/messages/:lat/:long', (req, res) => {
  const {lat, long} = req.params
  Message.find({
    location: {
      $near: {
        $geometry: {
          type: 'Point' ,
          coordinates: [ +lat , +long ]
       },
       $maxDistance: 100,
       $minDistance: 0
      }
    }
  }, (err, result) => {
    if(err || !result) {
      return res.status(500).send(err)
    } else if(!result.length) {
      return res.status(200).send('No messages!')
    }

    const messages = {result}

    return res.status(200).send(messages);
    
  })
})

app.get('/messages', (req, res) => {
  const {lat, long} = req.params
  Message.find({}, (err, result) => {
    if(err || !result) {
      return res.status(500).send(err)
    } else if(!result.length) {
      return res.status(200).send('No messages!')
    }

    const messages = {result}

    return res.status(200).send(messages);
    
  })
})

app.post('/messages', (req, res) => {
  console.log('UPLOADING MESSAGE')
  const {text, latitude, longitude, altitude, animation} = req.body
  
  console.log(animation)

  const animationParsed = JSON.parse(animation).frames

  if(!text || !latitude || !longitude || !altitude) {
    return res.status(500).send('Missing required parameters!')
  }

  const message = new Message({
    text,
    location: {
      type: 'Point',
      coordinates: [+latitude, +longitude]
    },
    altitude: +altitude,
    animation: animationParsed
  })

  message.save()
   .then(doc => {
     console.log('New message saved!')
     return res.status(200).send(message)
   })
   .catch(err => {
     console.error(err)
     return res.send(500).send(err)
   })
})

app.listen(port, () => console.log(`Example app listening on port ${port}!`))