const mongoose = require('mongoose')

const BoneTransform = new mongoose.Schema({
  rotation: {
    type: [Number],
    length: 4
  },
  position: {
    type: [Number],
    length: 3
  }
})

const MessageSchema = new mongoose.Schema({
  text: String,
  location: {
    type: {
      type: String,
      enum: ['Point'],
      required: true
    },
    coordinates: {
      type: [Number],
      required: true
    }
  },
  altitude: {
    type: Number,
    required: true
  },
  animation: {
    type: [[BoneTransform]],
    required: false,
    default: undefined
  },
  points: {
    type: Number,
    required: true,
    default: 0
  }
})

MessageSchema.index({location: '2dsphere'})

const Message = mongoose.model('Message', MessageSchema)

module.exports = Message