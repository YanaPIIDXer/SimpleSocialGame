require 'rails_helper'

RSpec.describe "Logins", type: :request do
  describe "Login" do
    it "Create User" do
      param = {name: "test"}
      json = param.to_json()
      post "/login", params: {body: json}
      user = User.find_by(name: "test")
      expect(user).not_to eq nil
      expect(user.stone).to eq 100
      res = JSON.parse(response.body)
      expect(res["stone"]).to eq 100
    end

    it "Null Request" do
      post "/login"
      expect(response).not_to have_http_status(:success)
    end

    it "Empty Name" do
      param = {name: ""}
      json = param.to_json()
      post "/login", params: {body: json}
      expect(response).not_to have_http_status(:success)
    end
  end
end
