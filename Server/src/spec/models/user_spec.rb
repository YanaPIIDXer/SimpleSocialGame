require 'rails_helper'

RSpec.describe User, type: :model do
  describe "Create User" do
    it "Create" do
      user = User.new(name: "test")
      expect(user.valid?).to be_truthy
    end

    it "Name must be unique" do
      user = User.new(name: "test")
      user.save

      user = User.new(name: "test")
      expect(user.valid?).to be_falsey
    end
  end
end
